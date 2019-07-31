using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBall : MonoBehaviour
{
    Renderer rend;
    public Rigidbody rb;
    public GameObject blackhole;
    public bool black = false;
    public bool white = true;

    public int wallHit = 0;

    public int taps = 0;
    public float timeSinceTap;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        GameManager.Instance.startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        blackhole = GameObject.FindWithTag("Black Hole");

        rb.velocity = new Vector3(0,10,0);

    }

        void Update()
    {
        //Switch ball color
        if (Input.GetKeyDown(KeyCode.C))
            ChangeColor();

        if (Input.touchCount > 0)
        {
            Touch tap = Input.GetTouch(0);
            if (tap.tapCount == 1 && tap.phase == TouchPhase.Ended)
            {
                if (taps == 0)
                {
                    Debug.Log("One Tap");
                    timeSinceTap = Time.time;
                    taps++;
                }
                else if (taps == 1 && (Time.time - timeSinceTap <= 0.5f) /* and done within the time */)
                {
                    //Double Tap
                    Debug.Log("Double Tap");
                    ChangeColor();
                    taps = 0;
                }
                else
                    taps = 0;
            }
        }

        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                if (Input.GetTouch(i).tapCount == 2)
                {
                    Debug.Log("Double Tap");
                    ChangeColor();
                }
            }
        }

        if (GameManager.Instance.bigBall){
            transform.localScale = new Vector3(1,1,1);        
        } else {
            transform.localScale = new Vector3(.5f,.5f,.5f);        
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Black Hole")){
            wallHit = 0;
            GameManager.Instance.lives--;
            if(GameManager.Instance.activeBlackHole){
                Destroy(gameObject);
            }
        } else if(collision.gameObject.CompareTag("Brick")){
            wallHit = 0;
        } else if(collision.gameObject.CompareTag("Ball")){
            wallHit = 0;
        } else if(collision.gameObject.CompareTag("Shield")){
            wallHit = 0;
            GameManager.Instance.isShield = false;
        } else if (collision.gameObject.CompareTag("Stage")){
            if(wallHit >=6){
                rb.velocity = new Vector3(0,0,0);
                transform.LookAt(blackhole.transform);
                rb.AddRelativeForce(Vector3.forward * 10, ForceMode.VelocityChange); 
            } else {
                wallHit++;
            }
        }        
    }

    void ChangeColor()
    {
        if(white)
        {
            rend.material.SetColor("_Color", Color.black);
            white = false; black = true;
        }
        else if(black)
        {
            rend.material.SetColor("_Color", Color.white);
            white = true; black = false;
        }
    }


}




// possible method for dynamic ball speed.
// increment speed in fixed update
// use add relative force in fixed updated based on speed, if under max speed.