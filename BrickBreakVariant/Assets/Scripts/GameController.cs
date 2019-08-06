using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set; }
    
    //Add Ball
    public Transform ball;

	private void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

	public void Update(){
		//for testing
		if (Input.GetKeyDown(KeyCode.R)){
            StartCoroutine(Respawn());
        }
		//for testing
		if (Input.GetKeyDown(KeyCode.C)){
            ChangeColor();
        }

		// Touch touchInput;
		// if(Input.touchCount == 3){
        //     ChangeColor();
        // }

		if(LevelLoader.Instance.isLoaded){
			StartCoroutine(Respawn());
			LevelLoader.Instance.isLoaded = false;
		} else if(GameManager.Instance.bricksLeft <= 0 && GameManager.Instance.activeGame){
			WinLevel();
		} else if(GameManager.Instance.numBalls <= 0 && GameManager.Instance.activeGame && GameManager.Instance.lives <=0){
			GameManager.Instance.activeGame = false;
			UIManager.Instance.Notify("GAME OVER", 5f);
            UIManager.Instance.OpenGameOverMenu();
		} else if(GameManager.Instance.numBalls <= 0 && GameManager.Instance.activeGame){
			GameManager.Instance.activeGame = false;
			GameManager.Instance.lives--;
			StartCoroutine(Respawn());
		}
	}
    public void AddBall () {
        Instantiate(ball, GameManager.Instance.ballVector, Quaternion.identity);
		GameManager.Instance.numBalls++;
    }

	public void ChangeColor() {
		Renderer rend;
		var balls = GameObject.FindGameObjectsWithTag("Ball");
		if(GameManager.Instance.greenBall) {
			for(var i = 0; i < balls.Length; i++){
				rend = balls[i].GetComponent<Renderer>();
				rend.material.SetColor("_Color", Color.blue);
				rend.material.SetColor("_EmissionColor", Color.blue);
			}
			GameManager.Instance.greenBall = false; 
			GameManager.Instance.blueBall = true;
		} else if(GameManager.Instance.blueBall) {
			for(var i = 0; i < balls.Length; i++){
				rend = balls[i].GetComponent<Renderer>();
				rend.material.SetColor("_Color", Color.green);
				rend.material.SetColor("_EmissionColor", Color.green);
			}
			GameManager.Instance.greenBall = true; 
			GameManager.Instance.blueBall = false;
		}
		AudioManager.Instance.PlayEffect("changeColor");
    }

	// public void Respawn (){
	// 	GameManager.Instance.ballVector = GameManager.Instance.startPos;
	// 	AddBall();
	// 	GameManager.Instance.activeGame = true;
	// }

	public IEnumerator Respawn (){
		// GameManager.Instance.numBalls++;
		yield return new WaitForSeconds(1);
		UIManager.Instance.Notify("3", 1);
		yield return new WaitForSeconds(1);
		UIManager.Instance.Notify("2", 1);
		yield return new WaitForSeconds(1);
		UIManager.Instance.Notify("1", 1);
		yield return new WaitForSeconds(1);
		GameManager.Instance.ballVector = GameManager.Instance.startPos;
		AddBall();
		GameManager.Instance.activeGame = true;
	}

	void WinLevel(){

		GameManager.Instance.activeGame = false;
		GameManager.Instance.onLevel++;
		UIManager.Instance.Notify("VICTORY", 5f);
		AudioManager.Instance.PlayEffect("win");
		var balls = GameObject.FindGameObjectsWithTag("Ball");
		for(var i = 0; i < balls.Length; i++){
			Destroy(balls[i]);
			GameManager.Instance.numBalls--;
		}
		var rotators = GameObject.FindGameObjectsWithTag("Rotator");
		for(var i = 0; i < rotators.Length; i++){
			rotators[i].transform.eulerAngles = new Vector3(0, 0, 0);
		}
		StartLevel(GameManager.Instance.onLevel);
	}

	public void StartLevel(int i){
		switch(i){
			case 1:
				LevelLoader.Instance.LoadLevel(LevelLoader.Instance.one);
				UIManager.Instance.Notify("LEVEL ONE", 1f);
				UIManager.Instance.Subtitle("The Basics", 1f);
				break;
			case 2:
				LevelLoader.Instance.LoadLevel(LevelLoader.Instance.two);
				UIManager.Instance.Notify("LEVEL TWO", 1f);
				UIManager.Instance.Subtitle("Some are Tougher", 1f);;
				break;
			case 3:
				LevelLoader.Instance.LoadLevel(LevelLoader.Instance.three);
				UIManager.Instance.Notify("LEVEL THREE", 1f);
				UIManager.Instance.Subtitle("The Contrarians", 1f);
				break;
			case 4:
				LevelLoader.Instance.LoadLevel(LevelLoader.Instance.four);
				UIManager.Instance.Notify("LEVEL FOUR", 1f);
				UIManager.Instance.Subtitle("Look at Them Weave", 1f);
				break;
			case 5:
				LevelLoader.Instance.LoadLevel(LevelLoader.Instance.five);
				UIManager.Instance.Notify("LEVEL FIVE", 1f);
				UIManager.Instance.Subtitle("Wallflowers", 1f);
				break;
			case 6:
				LevelLoader.Instance.LoadLevel(LevelLoader.Instance.six);
				UIManager.Instance.Notify("LEVEL SIX", 1f);
				UIManager.Instance.Subtitle("Double Tap to Change Colors", 1f);
				break;
			default:
				string message = "ENDLESS MODE: " + (GameManager.Instance.onLevel - 6).ToString();
				LevelLoader.Instance.GenerateLevel();
				UIManager.Instance.Notify(message, 1f);
				UIManager.Instance.Subtitle("There is No Rationality", 1f);
				break;
		}
	}

	public void UpdateScore(){
		GameManager.Instance.score += GameManager.Instance.multiplier * 100;
		GameManager.Instance.multiplier = 0;	
	}

	public void CheckHighScore(){
		if(GameManager.Instance.score > GameManager.Instance.highScore && !GameManager.Instance.isExtra){
			//set new highscore
			//update gameover menu to notify
		}
		if(GameManager.Instance.score > GameManager.Instance.extraHighScore){
			//set new highscore
			//update gameover menu to notify
		}
	}

}
