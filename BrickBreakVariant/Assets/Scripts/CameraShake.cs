using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cam;
    private Vector3 originalPos;
    public float duration = 2f;
    float shakeDur = 0f;
    public float shakeStr = 0.7f;
    public float decreaseFactor = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        if(cam == null){
            cam = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable(){
        originalPos = cam.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeDur > 0){
            cam.localPosition = originalPos + Random.insideUnitSphere * shakeStr;
            shakeDur -= Time.deltaTime * decreaseFactor;
        } else {
            shakeDur = 0f;
            cam.localPosition = originalPos;
        }
    }

    public void Trigger(){
        shakeDur = duration;
    }

}
