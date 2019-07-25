using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBrick : MonoBehaviour
{
    private Renderer rend;
    public GameController gc;
    private Collider mCollider;
    private BrickController brick;
 
    // Start is called before the first frame update
    void Start()
    {   
        brick = gameObject.GetComponent<BrickController>();  
        rend = GetComponent<Renderer>();
        gc = FindObjectOfType<GameController>();
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
            BaseBall ball = collision.gameObject.GetComponent<BaseBall>();
            if (brick.toughness == 0)
            {
                // gc.BrickDestroyed();
                // gc.UpdateScore(ball.streak, collision.gameObject.transform);
                brick.isActive = false;
                calcPowerUp(collision.gameObject);
            } else if(brick.black && ball.black || brick.white && ball.white){
                // gc.BrickDestroyed();
                // gc.UpdateScore(ball.streak, collision.gameObject.transform);
                brick.toughness--;
            } else if(!brick.isMatch){
                brick.toughness--;
            }
        }
    }

    private void calcPowerUp(GameObject ball){
        int powerup = Random.Range(0, 100);
        // Debug.Log(powerup);
        if(powerup == 0){
            //add a life
            GameManager.Instance.lives++;
            Debug.Log("life added");
        }
        if(powerup >= 1 && powerup <= 2) {
            //add balls
            GameManager.Instance.numBalls ++;
            GameManager.Instance.ballVector = ball.transform.position;
            gc.AddBall();
        }
        if(powerup >= 3 && powerup <= 4) {
            //shield
            GameManager.Instance.isShield = true;
            Debug.Log("shield active");
        }
        if(powerup >= 5 && powerup <= 6) {
            //speed
        }
        if(powerup >= 7 && powerup <= 8) {
            //ball size
            GameManager.Instance.bigBall = true;
            GameManager.Instance.totalBallTime += GameManager.Instance.ballTime;
            Debug.Log("big ball");
        }
        if(powerup >= 9 && powerup <= 10) {
            //score multiplier
            GameManager.Instance.multiplier++;
            Debug.Log("multiplier incremented");
        }
    }


}
