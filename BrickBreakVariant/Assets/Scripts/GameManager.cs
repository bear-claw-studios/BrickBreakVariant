using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance {get; private set; }

    public bool adPlaying = false;
    public bool activeGame = true;

    public Vector3 startPos;
    public int lives = 3;
    public int numBalls = 1;
    public int setSpeed = 8;
    public int setHyper = 12;
    public int bricksLeft;
    public int onLevel = 1;
    public int score;
    public int multiplier;
    public bool isShield = false;
    public bool bigBall = false;
    public bool hyperSpeed = false;

    public bool greenBall = true;
    public bool blueBall = false;

    //for testing
    public bool activeBlackHole = true;

    //big ball timer
    private float ballTimer = 0f;
    public float ballTime = 3f;
    public float totalBallTime;

    //hyper speed timer
    public float speedTimer = 0f;
    public float speedTime = 5f;
    public float totalSpeedTime;

    //add ball;
    public Vector3 ballVector;


    public int highScore;
    public int extraHighScore;
    public bool isExtra = false;


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
       GameController.Instance.StartLevel(onLevel);
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
        //hyper speed timer
        if(hyperSpeed){
            speedTimer += Time.deltaTime;
            if(speedTimer > totalSpeedTime){
                hyperSpeed = false;
                speedTimer = speedTimer - totalSpeedTime;
                totalSpeedTime = speedTime;
            }
        }
    }

    public void ResetGM(){
        var rotators = GameObject.FindGameObjectsWithTag("Rotator");
		for(var i = 0; i < rotators.Length; i++){
			rotators[i].transform.eulerAngles = new Vector3(0, 0, 0);
		}
        lives = 3;
        numBalls = 0;
        bricksLeft = 0;
        onLevel = 1;
        score = 0;
        multiplier = 0;
        isShield = false;
        bigBall = false;
        hyperSpeed = false;
        isExtra = false;
        greenBall = true;
        blueBall = false;
        totalBallTime = ballTime;
        totalSpeedTime = speedTime;
        GameController.Instance.StartLevel(onLevel);
    }
}
