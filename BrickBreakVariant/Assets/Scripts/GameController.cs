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
		if (Input.GetKeyDown(KeyCode.R)){
            StartCoroutine(Respawn());
        }
		if(GameManager.Instance.bricksLeft == 0 && GameManager.Instance.activeGame){
			WinLevel();
		}
		if(GameManager.Instance.numBalls <= 0 && GameManager.Instance.activeGame && GameManager.Instance.lives <=0){
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

	// public void Respawn (){
	// 	GameManager.Instance.ballVector = GameManager.Instance.startPos;
	// 	AddBall();
	// 	GameManager.Instance.activeGame = true;
	// }

	public IEnumerator Respawn (){
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
		StartLevel(GameManager.Instance.onLevel);
	}

	void StartLevel(int i){
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
			// case 3:
			// 	LevelLoader.Instance.LoadLevel(LevelLoader.Instance.three);
			// 	UIManager.Instance.Notify("LEVEL THREE", 1f);
			// 	UIManager.Instance.Subtitle("The Contrarians", 1f);
			// 	break;
			// case 4:
			// 	LevelLoader.Instance.LoadLevel(LevelLoader.Instance.four);
			// 	UIManager.Instance.Notify("LEVEL FOUR", 1f);
			// 	UIManager.Instance.Subtitle("Wallflowers", 1f);
			// 	break;
			// case 5:
			// 	LevelLoader.Instance.LoadLevel(LevelLoader.Instance.five);
			// 	UIManager.Instance.Notify("LEVEL FIVE", 1f);
			// 	UIManager.Instance.Subtitle("Double Tap to Change Colors", 1f);
			// 	break;
            }
			StartCoroutine(Respawn());

	}

	public void UpdateScore(){
		GameManager.Instance.score += GameManager.Instance.multiplier * 100;
		GameManager.Instance.multiplier = 0;	
	}

}
