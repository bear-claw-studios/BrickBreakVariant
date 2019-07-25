using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBrick : MonoBehaviour
{
    public GameControllerColorChange gc;
    private Renderer rend;
    private BrickController brick;   

    //For Fade
    private Collider mCollider;
    public float fadeTime = 2f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        brick = gameObject.GetComponent<BrickController>();  
        rend = GetComponent<Renderer>();
        mCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(brick.isFade){
            timer += Time.deltaTime;
            if(timer > fadeTime){
                mCollider.enabled = !mCollider.enabled;
                Color color = rend.material.color;
                if(color.a == 1){
                    color.a = 0.5f;
                } else {
                    color.a = 1f;
                }
                rend.material.color = color;
                timer = timer - fadeTime;
            }
        }        
    }
}
