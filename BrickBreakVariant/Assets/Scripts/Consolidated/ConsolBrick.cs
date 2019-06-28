using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rendering mode for brick material must be "Fade"

public class ConsolBrick : MonoBehaviour
{
    public string id;
    public GameControllerColorChange gc;
    private Renderer rend;
    //Brick type
    public bool isActive = false;
    public bool isFade = false;
    public bool isMatch = false;
    public int toughness = 1;

    //For Fade
    private Collider mCollider;
    public float fadeTime = 2f;
    private float timer = 0f;

    //For Match
    public bool black;
    public bool white;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        mCollider = GetComponent<Collider>();
        gc = FindObjectOfType<GameControllerColorChange>();
        //Setting Brick to inactive on start
        mCollider.enabled = false;
        Color color = rend.material.color;
        color.a = 0f;
        rend.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        //Fade Code for brick
        if(isFade){
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
        // Sets Brick Colors
        // For Match Bricks
        if(isMatch){
            if(black){
                rend.material.SetColor("_Color", Color.black);
            } else {
                rend.material.SetColor("_Color", Color.white);
            }
        }
        //For Multi-hit Bricks
        if (toughness == 0)
        {
            Color color = rend.material.color;
            color.r = 1f; //red
            rend.material.color = color; 
            // rend.material.SetColor("_Color", Color.red);
        }
        if (toughness == 1)
        {
            Color color = rend.material.color;
            color.r = 1f;
            color.b = 1f; //red and blue for magenta
            rend.material.color = color; 
            // rend.material.SetColor("_Color", Color.magenta);
        }
        if (toughness == 2)
        {
            Color color = rend.material.color;
            color.b = 1f; //blue
            rend.material.color = color; 
            // rend.material.SetColor("_Color", Color.blue);
        } 

        //activates brick or "destroys" it
        if(isActive){
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BallColorChanger ball = collision.gameObject.GetComponent<BallColorChanger>();
            if (toughness == 0)
            {
                gc.BrickDestroyed();
                // gc.UpdateScore(ball.streak, collision.gameObject.transform);
                isActive = false;
            } else if(black && ball.black || white && ball.white){
                gc.BrickDestroyed();
                // gc.UpdateScore(ball.streak, collision.gameObject.transform);
                isActive = false;
            } else {
                toughness--;
            }
        }
    }
}