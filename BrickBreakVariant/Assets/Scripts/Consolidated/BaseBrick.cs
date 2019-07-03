using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBrick : MonoBehaviour
{
    private Renderer rend;
    public GameControllerColorChange gc;
    private Collider mCollider;
    private BrickController brick;   
  
    // Start is called before the first frame update
    void Start()
    {   
        brick = gameObject.GetComponent<BrickController>();  
        rend = GetComponent<Renderer>();
        gc = FindObjectOfType<GameControllerColorChange>();
        mCollider = GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        if(brick.isMatch){
            if(brick.black){
                Color color = rend.material.color;
                color.r = 0f; 
                color.g = 0f;
                color.b = 0f;
                color.a = 1f;
                rend.material.color = color;
                // rend.material.SetColor("_Color", Color.black);
            } else {
                Color color = rend.material.color;
                color.r = 1f; 
                color.g = 1f;
                color.b = 1f;
                color.a = 1f;
                rend.material.color = color;
                // rend.material.SetColor("_Color", Color.white);
            }
        } else {
            if (brick.toughness == 0) {
                    Color color = rend.material.color;
                    color.r = 1f; //red
                    color.g = 0f;
                    color.b = 0f;
                    rend.material.color = color; 
                    // rend.material.SetColor("_Color", Color.red);
            }
            if (brick.toughness == 1) {
                Color color = rend.material.color;
                color.r = 1f;
                color.g = 0f;
                color.b = 1f; //red and blue for magenta
                rend.material.color = color; 
                // rend.material.SetColor("_Color", Color.magenta);
            }
            if (brick.toughness == 2) {
                Color color = rend.material.color;
                color.r = 0f;
                color.g = 0f;
                color.b = 1f; //blue
                rend.material.color = color; 
                // rend.material.SetColor("_Color", Color.blue);
            }
        }
        
    }
        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BallColorChanger ball = collision.gameObject.GetComponent<BallColorChanger>();
            if (brick.toughness == 0)
            {
                gc.BrickDestroyed();
                // gc.UpdateScore(ball.streak, collision.gameObject.transform);
                brick.isActive = false;
            } else if(brick.black && ball.black || brick.white && ball.white){
                gc.BrickDestroyed();
                // gc.UpdateScore(ball.streak, collision.gameObject.transform);
                brick.toughness--;
            } else if(!brick.isMatch){
                brick.toughness--;
            }
        }
    }
}
