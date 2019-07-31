using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{

    public string id;
    private Renderer rend;
    private Collider mCollider;

    //Brick type
    public bool isActive = false;
    public bool isFade = false;
    public bool isMatch = false;
    public int toughness = 1;

    //For Fade
    public float fadeTime = 2f;
    private float timer = 0f;
    
    //For Match
    public bool blue;
    public bool green;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        mCollider = GetComponent<Collider>();
        //Setting Brick to inactive on start
        mCollider.enabled = false;
        Color color = rend.material.color;
        color.a = 0f;
        rend.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && isFade){
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
        } else if(isActive) {
            mCollider.enabled = true;
            Color color = rend.material.color;
            color.a = 1f;
            rend.material.color = color;            
        } else {
            mCollider.enabled = false;
            Color color = rend.material.color;
            color.a = 0f;
            rend.material.color = color;  
        }
    }
}
