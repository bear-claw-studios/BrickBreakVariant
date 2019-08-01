using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set; }

    public Text scoreText, streakText, livesText, brickText, notificationText, subtitleText;

    void Awake(){
        if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
    }
    void Update(){
        livesText.text = "Extra Lives: " + GameManager.Instance.lives;
        brickText.text = "Bricks Left: " + GameManager.Instance.bricksLeft;
        scoreText.text = "Score: " + GameManager.Instance.score;
        streakText.text = "Multiplier: " + GameManager.Instance.multiplier;
    }

    public void Notify(string message, float timer){
        notificationText.text = message;
        StartCoroutine(ResetNotify(timer));
    }

    public void Subtitle(string message, float timer){
        subtitleText.text = message;
        StartCoroutine(ResetSubtitle(timer));
    }

    IEnumerator ResetNotify(float timer){
        yield return new WaitForSeconds(timer);
        notificationText.text = "";
    }

    IEnumerator ResetSubtitle(float timer){
        yield return new WaitForSeconds(timer);
        subtitleText.text = "";
    }

}
