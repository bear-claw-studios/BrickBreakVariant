﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set; }
    //game ui
    public Text scoreText, streakText, livesText, brickText, notificationText, subtitleText;
    //menu ui
    public Text highScore, extraHighScore;
    //game over high score text
    public Text goHighScore, goExtraHighScore;

    public GameObject mainMenu, settingsMenu, creditsMenu, gameOverMenu, quitMenu;

    public Button exitButton;

    //flash panel
    public  CanvasGroup flashPanel;
    public float flashDur = 1f;

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
        quitMenu.SetActive(false);
        UpdateHighScores();
        flashPanel.interactable = false;
        flashPanel.blocksRaycasts = false;
        flashPanel.alpha = 0f;
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
        UpdateHighScores();
    }

    public void WatchAd()
    {
        AdController.Instance.WatchAdvert();
        exitButton.enabled = false;
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        if(gameOverMenu.activeInHierarchy)
            gameOverMenu.SetActive(false);
        if (settingsMenu.activeInHierarchy)
            settingsMenu.SetActive(false);
        Pause();
        GameManager.Instance.ResetGM();
        UpdateHighScores();
    }

    public void OpenQuitMenu()
    {
        quitMenu.SetActive(true);
        Pause();
    }

    public void ExitQuitMenu()
    {
        quitMenu.SetActive(false);
        Resume();
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void UpdateHighScores()
    {
        highScore.text = "No Help High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
        extraHighScore.text = "Help High Score: " + PlayerPrefs.GetInt("ExtraHighScore").ToString();
        goHighScore.text = "No Help High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
        goExtraHighScore.text = "Help High Score: " + PlayerPrefs.GetInt("ExtraHighScore").ToString();
    }

    public IEnumerator DeathFlash(){
        float countdown = flashDur;
        for(float t = flashDur; t > 0f; t -= Time.deltaTime){
            flashPanel.alpha = t;
            yield return null;
        }
        flashPanel.alpha = 0;           
    }
}
