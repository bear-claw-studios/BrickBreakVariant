using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int numBricks;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
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
            GameOver();
        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Bricks: " + numBricks.ToString();
    }

    void GameOver()
    {

    }
}
