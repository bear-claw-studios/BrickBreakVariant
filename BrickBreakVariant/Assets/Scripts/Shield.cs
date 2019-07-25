using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Renderer rend;
    private Collider mCollider;
    
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
        if(GameManager.Instance.isShield) {
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
