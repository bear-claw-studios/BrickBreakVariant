using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int numBricks;
    public Text scoreText, gameOverText;

    //Reference to the ball script
    public Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        UpdateScore();
        gameOverText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BrickDestroyed()
    {
        numBricks--;
        UpdateScore();
        if(numBricks == 0)
        {
            LevelComplete();
        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Bricks: " + numBricks;
    }

    void LevelComplete()
    {
        gameOverText.text = "Level Complete!";
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
