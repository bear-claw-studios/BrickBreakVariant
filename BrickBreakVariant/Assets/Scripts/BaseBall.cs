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
        rend.material.SetColor("_Color", Color.green);
        rend.material.SetColor("_EmissionColor", Color.green);
        GameManager.Instance.startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        blackhole = GameObject.FindWithTag("Black Hole");

        rb.velocity = new Vector3(0,10,0);

    }

        void Update()
    {
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
        if(green)
        {
            rend.material.SetColor("_Color", Color.blue);
            rend.material.SetColor("_EmissionColor", Color.blue);
            green = false; blue = true;
        }
        else if(blue)
        {
            rend.material.SetColor("_Color", Color.green);
            rend.material.SetColor("_EmissionColor", Color.green);
            green = true; blue = false;
        }
    }


}




// possible method for dynamic ball speed.
// increment speed in fixed update
// use add relative force in fixed updated based on speed, if under max speed.