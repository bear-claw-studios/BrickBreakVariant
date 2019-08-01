using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set; }

    public Text scoreText, streakText, livesText, brickText, notificationText, subtitleText;

    public GameObject mainMenu, settingsMenu, creditsMenu, gameOverMenu;

    void Awake(){
        if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
    }

    void Start()
    {
        mainMenu.SetActive(true);
        Pause();
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        gameOverMenu.SetActive(false);
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

    public void StartGame()
    {
        mainMenu.SetActive(false);
        Resume();
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        Pause();
    }

    public void ExitSettingsMenu()
    {
        settingsMenu.SetActive(false);
        if(!mainMenu.activeInHierarchy)
            Resume();
    }

    public void OpenCreditsMenu()
    {
        creditsMenu.SetActive(true);
        Pause();
    }

    public void ExitCreditsMenu()
    {
        creditsMenu.SetActive(false);
        if (!mainMenu.activeInHierarchy)
            Resume();
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
    }

    public void OpenGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void WatchAd()
    {

    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        Pause();
    }
}
