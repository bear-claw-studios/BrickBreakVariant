using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickColorChanger : MonoBehaviour
{
    public GameControllerColorChange gc;
    public int toughness;

    Renderer rend;

    public bool black, white;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        gc = FindObjectOfType<GameControllerColorChange>();

        if (gameObject.CompareTag("Black"))
        {
            black = true;
            white = false;
        }
        if (gameObject.CompareTag("White"))
        {
            black = false;
            white = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (toughness == 0)
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
                //gc.UpdateScore();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BallColorChanger ball = collision.gameObject.GetComponent<BallColorChanger>();

            if (toughness == 0)
            {
                gc.BrickDestroyed();
                gc.UpdateScore(ball.streak, collision.gameObject.transform);
                Destroy(gameObject);
            }
            else if ((black && ball.black) || white && ball.white)
            {
                //if (toughness > 0)
                //{
                    toughness--;
                    //}
                    /*
                    else
                    {
                        gc.BrickDestroyed();
                        gc.UpdateScore(ball.streak, collision.gameObject.transform);
                        Destroy(gameObject);
                    }
                    */
                //}
            }
        }
    }
}
