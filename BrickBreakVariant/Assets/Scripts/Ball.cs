using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 2.0f;
    public Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Respawn();
    }

    void Update()
    {
        //In case it gets stuck on the wall...
        if (Input.GetKeyDown(KeyCode.R))
            Respawn();
    }

    void Respawn()
    {
        transform.position = startPos;
        //GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle.normalized * speed;
        GetComponent<Rigidbody>().velocity = Random.insideUnitCircle.normalized * speed; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Black Hole"))
        {
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            Respawn();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Black Hole"))
        {
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            Respawn();
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
