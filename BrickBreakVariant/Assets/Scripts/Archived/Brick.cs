using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameController gc;
    public int toughness;

    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        // gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(toughness == 0)
        {
            rend.material.SetColor("_Color", Color.red);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (toughness > 0)
            {
                toughness--;
            }
            else
            {
                // gc.UpdateScore();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (toughness > 0)
            {
                toughness--;
            }
            else
            {
                // gc.BrickDestroyed();
                Destroy(gameObject);
            }
        }
    }
}
