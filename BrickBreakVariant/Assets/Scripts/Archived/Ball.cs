using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;

    public float speed = 2.0f;
    public float gravityForce = 10.0f;

    public Vector3 startPos;

    Renderer rend;
    Color defaultColor;

    GameController gc;

    //ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        // gc = FindObjectOfType<GameController>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        rend = GetComponent<Renderer>();
        Respawn();
        //ps = GetComponent<ParticleSystem>();
        //ps.Pause();
        defaultColor = rend.material.color;
    }

    void Update()
    {
        //In case it gets stuck on the wall...
        if (Input.GetKeyDown(KeyCode.R))
            Respawn();
    }

    public void Respawn()
    {
        rend.material.SetColor("_Color", Color.white);
        gameObject.SetActive(true);
        transform.position = startPos;
        //GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * speed;
        rb.velocity = Random.insideUnitCircle.normalized * speed; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Black Hole"))
        {
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            Respawn();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Gravity Field"))
        {
            //Debug.Log("Trigger entered");
            rend.material.SetColor("_Color", Color.black);
            //ps.Play();
            //Distance is the black hole's position (at the origin) minus the ball's current 
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 distance = Vector3.zero - transform.position;

            //rb.AddForce(distance.normalized * gravityForce);
            rb.velocity = distance.normalized * gravityForce;
        }
        else if (collider.gameObject.CompareTag("Black Hole"))
        {
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            //Respawn();
            gameObject.SetActive(false);
            // gc.GameOver();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.CompareTag("Gravity Field"))
        {
            rend.material.SetColor("_Color", defaultColor);
            //ps.Stop();
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    /*
     * 
     * 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Stage"))
        {
            Vector2 currentPos = transform.position;
            Vector2 returnDir = -(Vector2.zero - currentPos);
            GetComponent<Rigidbody2D>().velocity = returnDir;
        }
    }
    */
}
