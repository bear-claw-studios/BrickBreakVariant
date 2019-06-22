using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameController gc;
    public int toughness = 1;
    public bool isFade = false;

    //For fading
    private Collider mCollider;
    private MeshRenderer mRenderer;
    public float fadeTime = 2f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        mCollider = GetComponent<Collider>();
        mRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFade){
            timer += Time.deltaTime;
            if(timer > fadeTime){
                mCollider.enabled = !mCollider.enabled;
                Color color = mRenderer.material.color;
                if(color.a == 1){
                    color.a = 0.5f;
                } else {
                    color.a = 1f;
                }
                mRenderer.material.color = color;
                timer = timer - fadeTime;
            }
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if(toughness > 0){
                toughness--;
            } else {
                gc.UpdateScore();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if(toughness > 0){
                toughness--;
            } else {
                gc.UpdateScore();
                Destroy(gameObject);
            }
        }
    }

    private void calcPowerUp(){
        int powerup = Random.Range(0, 100);
        if(powerup == 0){
            //add a life
        }
        if(powerup >= 1 && powerup <= 2) {
            //double balls
        }
        if(powerup >= 3 && powerup <= 4) {
            //shield
        }
        if(powerup >= 5 && powerup <= 6) {
            //speed
        }
        if(powerup >= 7 && powerup <= 8) {
            //ball size
        }
        if(powerup >= 9 && powerup <= 10) {
            //score multiplier
        }
    }

}

