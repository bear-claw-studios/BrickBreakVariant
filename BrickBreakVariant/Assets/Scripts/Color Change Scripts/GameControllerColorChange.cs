using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerColorChange : MonoBehaviour
{
    public int numBricks;
    public int score = 0;
    public Text scoreText, gameOverText, countText;

    //Reference to the ball script
    public BallColorChanger ball;

    //GameObject stage;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<BallColorChanger>();
        UpdateScore(0);
        gameOverText.text = "";
        //LoadLevel();
    }

    // Update is called once per frame
    void Update()
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
    }

    void LevelComplete()
    {
        gameOverText.text = "Level Complete!";
        LoadLevel();
        //Reload level
    }

    public void GameOver()
    {
        gameOverText.text = "Whoops";
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3.0f);
        ball.Respawn();
        gameOverText.text = "";
    }
}
