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
            if(brick.green){
                Color color = rend.material.color;
                color.r = 0f; 
                color.g = 1f; //green
                color.b = 0f;
                color.a = 1f;
                rend.material.color = color;
                rend.material.SetColor("_EmissionColor", color);
                brick.color = "green";
            } else {
                Color color = rend.material.color;
                color.r = 0f; 
                color.g = 0f;
                color.b = 1f; //blue
                color.a = 1f;
                rend.material.color = color;
                rend.material.SetColor("_EmissionColor", color);
                brick.color = "blue";
            }
        } else {
            if (brick.toughness == 0) {
                Color color = rend.material.color;
                color.r = 1f; //red
                color.g = 0f;
                color.b = 0f;
                rend.material.color = color; 
                rend.material.SetColor("_EmissionColor", color);
                brick.color = "red";
            }
            if (brick.toughness == 1) {
                Color color = rend.material.color;
                color.r = 1f;
                color.g = 0.64f; //orange
                color.b = 0f; 
                rend.material.color = color; 
                rend.material.SetColor("_EmissionColor", color);
                brick.color = "orange";
            }
            if (brick.toughness == 2) {
                Color color = rend.material.color;
                color.r = 1f;
                color.g = 0.92f; //yellow
                color.b = 0.016f; 
                rend.material.color = color; 
                rend.material.SetColor("_EmissionColor", color);
                brick.color = "yellow";
            }
        }
        
    }
        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BaseBall ball = collision.gameObject.GetComponent<BaseBall>();
            if (brick.toughness == 0 && !brick.isMatch)
            {
                brick.isActive = false;
                brick.isFade = false;
                GameManager.Instance.bricksLeft--;
                calcPowerUp(collision.gameObject);
                collision.gameObject.GetComponent<BallAudio>().ballContact("break");
            } else if(brick.toughness == 0 && (brick.green && GameManager.Instance.greenBall || brick.blue && GameManager.Instance.blueBall)){
                brick.isActive = false;
                brick.isMatch = false;
                GameManager.Instance.bricksLeft--;
                calcPowerUp(collision.gameObject);
                collision.gameObject.GetComponent<BallAudio>().ballContact("break");
            } else if(brick.green && GameManager.Instance.greenBall || brick.blue && GameManager.Instance.blueBall){
                brick.toughness--;
                collision.gameObject.GetComponent<BallAudio>().ballContact("bounce");
            } else if(!brick.isMatch){
                brick.toughness--;
                collision.gameObject.GetComponent<BallAudio>().ballContact("bounce");
            } else {
                collision.gameObject.GetComponent<BallAudio>().ballContact("bounce");
            }
            //trigger sparks on
            collision.gameObject.GetComponentInChildren<SparkController>().TriggerSpark(brick.color);    
        }
    }

    private void calcPowerUp(GameObject ball){
        int powerup = Random.Range(0, 100);
        // Debug.Log(powerup);
        if(powerup == 0){
            //add a life
            GameManager.Instance.lives++;
            Debug.Log("life added");
            AudioManager.Instance.PlayEffect("extraLife");
            UIManager.Instance.Notify("Extra Life!", .5f);
        }
        if(powerup >= 1 && powerup <= 2) {
            //add balls
            GameManager.Instance.ballVector = ball.transform.position;
            gc.AddBall();
            AudioManager.Instance.PlayEffect("powerup");
            UIManager.Instance.Notify("Extra Ball!", .5f);
        }
        if(powerup >= 3 && powerup <= 4) {
            //shield
            GameManager.Instance.isShield = true;
            Debug.Log("shield active");
            AudioManager.Instance.PlayEffect("powerup");
            UIManager.Instance.Notify("Shield Active!", .5f);
        }
        if(powerup >= 5 && powerup <= 6) {
            GameManager.Instance.hyperSpeed = true;
            GameManager.Instance.totalSpeedTime += GameManager.Instance.speedTime;
            Debug.Log("hyper speed active");
            AudioManager.Instance.PlayEffect("powerup");
            UIManager.Instance.Notify("Hyper Speed!", .5f);
            //speed
        }
        if(powerup >= 7 && powerup <= 8) {
            //ball size
            GameManager.Instance.bigBall = true;
            GameManager.Instance.totalBallTime += GameManager.Instance.ballTime;
            Debug.Log("big ball");
            AudioManager.Instance.PlayEffect("powerup");
            UIManager.Instance.Notify("Mega Ball!", .5f);
        }
        if(powerup >= 9 && powerup <= 10) {
            //score multiplier
            GameManager.Instance.multiplier+= 25;
            Debug.Log("multiplier incremented");
            AudioManager.Instance.PlayEffect("powerup");
            UIManager.Instance.Notify("Multiplier!", .5f);
        }
    }


}
