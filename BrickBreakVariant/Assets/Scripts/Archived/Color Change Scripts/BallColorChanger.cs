﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallColorChanger : MonoBehaviour
{
    Rigidbody rb;

    public float speed = 2.0f;
    public float gravityForce = 10.0f;

    public Vector3 startPos;

    Renderer rend;
    Color defaultColor;

    GameControllerColorChange gc;

    public bool black, white;

    public int lives = 3;

    public int streak = 0;

    public int wallHits = 0;

    public Text streakText, livesText;

    public GameObject blackHole;

    public float maxSpeed;

    public int taps = 0;
    public float timeSinceTap;

    //ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameControllerColorChange>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        rend = GetComponent<Renderer>();
        Respawn();
        //ps = GetComponent<ParticleSystem>();
        //ps.Pause();
        defaultColor = rend.material.color;
        white = true; black = false;
        UpdateStreak();
        livesText.text = "Lives: " + lives; 
    }

    void Update()
    {
        Touch tap = Input.GetTouch(0);
        if(tap.phase == TouchPhase.Ended)
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

        //In case it gets stuck on the wall...
        if (Input.GetKeyDown(KeyCode.R))
            Respawn();

        /*
        if (Input.GetKeyDown(KeyCode.C))
            ChangeColor();
        */

        /*
        if(rb.velocity >= maxSpeed)
        {
            rb.velocity = maxSpeed;
        }
        */
    }

    private void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);
    }

    public void Respawn()
    {
        rend.material.SetColor("_Color", Color.white);
        white = true; black = false;
        gameObject.SetActive(true);
        transform.position = startPos;
        //GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * speed;
        //rb.velocity = Random.insideUnitCircle.normalized * speed;
        //rb.velocity = Vector3.down * speed;
        rb.AddForce(transform.forward * speed);
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
        /*
        if (collider.gameObject.CompareTag("Gravity Field"))
        {
            //Debug.Log("Trigger entered");
            //rend.material.SetColor("_Color", Color.black);
            //ps.Play();
            //Distance is the black hole's position (at the origin) minus the ball's current 
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            Vector3 distance = Vector3.zero - transform.position;

            //rb.AddForce(distance.normalized * gravityForce);
            rb.velocity = distance.normalized * gravityForce;
        }
        */
        /*else*/ if (collider.gameObject.CompareTag("Black Hole"))
        {
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            //Respawn();
            gameObject.SetActive(false);
            lives--;
            UpdateLives();
            if (lives == 0)
                gc.GameOver();
            else
                gc.LifeLost();
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Gravity Field"))
        {
            rend.material.SetColor("_Color", defaultColor);
            //ps.Stop();
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Brick"))
        {
            streak++;
            wallHits = 0;
            UpdateStreak();
        }
        else if (collision.gameObject.CompareTag("Stage"))
        {
            //gc.UpdateScore(streak);
            UpdateStreak();
            streak = 0;
            wallHits++;

            if(wallHits >= 3)
            {
                Vector3 currentPos = transform.position;
                Vector3 returnDir = -(Vector3.zero - currentPos);         
                transform.LookAt(blackHole.transform);
                //rb.AddForce(returnDir.normalized * speed);
                rb.AddForce(transform.forward * speed);
            }
        }
    }



    void UpdateStreak()
    {
        streakText.text = "Streak: " + streak + "x";
    }

    void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
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
