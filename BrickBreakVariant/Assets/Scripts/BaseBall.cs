using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBall : MonoBehaviour
{
    Renderer rend;
    public Rigidbody rb;
    public GameObject blackhole;
    public bool blue = false;
    public bool green = true;

    public int wallHit = 0;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        if(GameManager.Instance.greenBall){
            rend.material.SetColor("_Color", Color.green);
            rend.material.SetColor("_EmissionColor", Color.green);
        } else {
            rend.material.SetColor("_Color", Color.blue);
            rend.material.SetColor("_EmissionColor", Color.blue);
        }
        GameManager.Instance.startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        blackhole = GameObject.FindWithTag("Black Hole");

        // rb.velocity = new Vector3(0,10,0);
        rb.velocity = new Vector3(0,8,0);


    }

        void Update()
    {
        Touch touchInput;
        if(Input.touchCount == 2)
        {
            RedirectBall();
        }
        else if(Input.touchCount == 3)
        {
            ChangeColor();
        }

        //Switch ball color
        if (Input.GetKeyDown(KeyCode.C)){
            ChangeColor();
        }

        if(GameManager.Instance.bigBall){
            transform.localScale = new Vector3(.75f,.75f,.75f);        
        } else {
            transform.localScale = new Vector3(.5f,.5f,.5f);        
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(GameManager.Instance.)
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Black Hole")){
            AudioManager.Instance.PlayEffect("loss");
            wallHit = 0;
            UIManager.Instance.Notify("Whoops!!", 1);
            GameManager.Instance.numBalls--;
            GameManager.Instance.multiplier = 0;
            if(GameManager.Instance.activeBlackHole){
                Destroy(gameObject);
            }
        } else if(collision.gameObject.CompareTag("Brick")){
            GameManager.Instance.multiplier++;
            wallHit = 0;
        } else if(collision.gameObject.CompareTag("Ball")){
            wallHit = 0;
        } else if(collision.gameObject.CompareTag("Shield")){
            wallHit = 0;
            GameManager.Instance.isShield = false;
            GetComponent<BallAudio>().ballContact("break");
        } else if (collision.gameObject.CompareTag("Stage")){
            //this doesn't work again
            //hopefully a redirect won't be necessary, with the improved timestep
            // if(wallHit >= 6){
            //     rb.velocity = new Vector3(0,0,0);
            //     transform.LookAt(blackhole.transform);
            //     rb.AddRelativeForce(Vector3.forward * 10, ForceMode.VelocityChange); 
            // } else {
            //     wallHit++;
            // }
            GameController.Instance.UpdateScore();
            wallHit++;
            GetComponent<BallAudio>().ballContact("bounce");
        }        
    }

    void ChangeColor()
    {
        if(GameManager.Instance.greenBall)
        {
            rend.material.SetColor("_Color", Color.blue);
            rend.material.SetColor("_EmissionColor", Color.blue);
            GameManager.Instance.greenBall = false; 
            GameManager.Instance.blueBall = true;
            AudioManager.Instance.PlayEffect("changeColor");
        }
        else if(GameManager.Instance.blueBall)
        {
            rend.material.SetColor("_Color", Color.green);
            rend.material.SetColor("_EmissionColor", Color.green);
            GameManager.Instance.greenBall = true; 
            GameManager.Instance.blueBall = false;
            AudioManager.Instance.PlayEffect("changeColor");
        }
    }

    void RedirectBall()
    {
        rb.velocity = new Vector3(0, 0, 0);
        transform.LookAt(blackhole.transform);
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.VelocityChange); 
    }
}




// possible method for dynamic ball speed.
// increment speed in fixed update
// use add relative force in fixed updated based on speed, if under max speed.