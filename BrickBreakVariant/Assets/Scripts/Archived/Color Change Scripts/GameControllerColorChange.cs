using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerColorChange : MonoBehaviour
{
    public int numBricks;
    public int score = 0;
    public int highScore = 0;
    public Text scoreText, gameOverText, countText, highScoreText;
    Vector3 prevVelocity;

    AudioSource audio;

    public GameObject pauseMenu;

    //Reference to the ball script
    public BallColorChanger ball;

    //GameObject stage;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<BallColorChanger>();
        UpdateScore(0);
        gameOverText.text = "";
        highScoreText.text = "High score: " + highScore;
        audio = GetComponent<AudioSource>();
        //LoadLevel();
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMusicVolume(float volume)
    {
        audio.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {

    }

    public void LoadLevel()
    {
        //Instantiate(stage);
    }

    public void BrickDestroyed()
    {
        numBricks--;
        //UpdateScore();
        if (numBricks == 0)
        {
            LevelComplete();
        }
    }

    public void UpdateScore(int streak)
    {
        countText.text = "Bricks: " + numBricks;
        score += 100 * streak;
        scoreText.text = "Score: " + score;

        if(score > highScore)
        {
            highScore = score;
            highScoreText.text = "High score: " + highScore;
        }
    }

    public void UpdateScore(int streak, Transform location)
    {
        //Debug.Log(location.position);
        countText.text = "Bricks: " + numBricks;
        score += 100 * streak;
        scoreText.text = "Score: " + score;
    }

    void LevelComplete()
    {
        gameOverText.text = "Level Complete!";
        //LoadLevel();
        //Reload level
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        //StartCoroutine(Wait());

        //Insert eventual ad 
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        ball.Respawn();
        gameOverText.text = "";
    }

    public void LifeLost()
    {
        gameOverText.text = "Whoops";
        StartCoroutine(Wait());
    }

    public void PauseGame()
    {
        prevVelocity = ball.GetComponent<Rigidbody>().velocity;
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        audio.Pause();
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        ball.GetComponent<Rigidbody>().velocity = prevVelocity;
        audio.UnPause();
        pauseMenu.SetActive(false);
    }
}
