using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance {get; private set; }

    public Vector3 startPos;
    public int lives = 3;
    public int numBalls = 1;
    // public float forceSpeed = .1f;
    // public float maxBallSpeed = 200f;
    public int bricksLeft;
    public int onLevel;
    public int score;
    public int multiplier;
    public bool isShield = false;
    public bool bigBall = false;
    // public int tutorialLevel;
    // public bool isExtra = false;

    //for testing
    public bool activeBlackHole = true;

    //big ball timer
    private float ballTimer = 0f;
    public float ballTime = 10f;
    public float totalBallTime;

    //add ball;
    public Vector3 ballVector;


    public int highScore;
    // public int extraHighScore;

	private void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	private void Start () {
       totalBallTime = ballTime; 
	}

    // Update is called once per frame
    void Update()
    {
        //big ball timer
        if(bigBall){
            ballTimer += Time.deltaTime;
            if(ballTimer > totalBallTime){
                bigBall = false;
                ballTimer = ballTimer - totalBallTime;
                totalBallTime = ballTime;
            }
        }
    }
}
