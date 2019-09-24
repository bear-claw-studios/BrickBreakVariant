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
        rb = GetComponent<Rigidbody>();
        blackhole = GameObject.FindWithTag("Black Hole");

        // rb.velocity = new Vector3(0,10,0);
        rb.velocity = new Vector3(0,GameManager.Instance.setSpeed,0);


    }

        void Update()
    {
        // Debug.Log(rb.velocity.magnitude);
         Touch touchInput;
         if(Input.touchCount == 2)
         {
             StartCoroutine(NewVel());
         }

		if (Input.GetKeyDown(KeyCode.F)){
            StartCoroutine(NewVel());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //maintains speed and sets hyper speed
        if(rb.velocity.magnitude < GameManager.Instance.setSpeed){
            // Debug.Log("speed was: " + rb.velocity.magnitude + ", now reset");
            rb.velocity = rb.velocity.normalized * GameManager.Instance.setSpeed;
        }
        if(GameManager.Instance.hyperSpeed){
            rb.velocity = rb.velocity.normalized * GameManager.Instance.setHyper;
        } else if(!GameManager.Instance.hyperSpeed && rb.velocity.magnitude > GameManager.Instance.setSpeed){
            rb.velocity = rb.velocity.normalized * GameManager.Instance.setSpeed;
        }

        //sets ball size
        if(GameManager.Instance.bigBall){
            transform.localScale = new Vector3(.75f,.75f,.75f);        
        } else {
            transform.localScale = new Vector3(.5f,.5f,.5f);        
        }

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
            GameManager.Instance.hyperSpeed = false;
            GameManager.Instance.totalSpeedTime = GameManager.Instance.speedTime;
            GameManager.Instance.speedTimer = 0;
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
            if(wallHit >= 6){
                StartCoroutine(NewVel());
            } else {
                wallHit++;
            }
            GameController.Instance.UpdateScore();
            GetComponent<BallAudio>().ballContact("bounce");
        }        
    }

    // void RedirectBall()
    // {
    //     rb.velocity = new Vector3(0, 0, 0);
    //     transform.LookAt(blackhole.transform);
    //     rb.AddRelativeForce(Vector3.forward * 10, ForceMode.VelocityChange); 
    // }

    IEnumerator NewVel(){
        yield return new WaitForFixedUpdate();
        rb.velocity = new Vector3(0,0,0);
        transform.LookAt(blackhole.transform);
        yield return new WaitForFixedUpdate();
        if(GameManager.Instance.hyperSpeed){
            rb.AddRelativeForce(Vector3.forward * GameManager.Instance.setHyper, ForceMode.VelocityChange); 
        } else {
            rb.AddRelativeForce(Vector3.forward * GameManager.Instance.setSpeed, ForceMode.VelocityChange); 
        }
    }
}




// possible method for dynamic ball speed.
// increment speed in fixed update
// use add relative force in fixed updated based on speed, if under max speed.