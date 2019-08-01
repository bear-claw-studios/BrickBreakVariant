using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public GameObject settingsMenu, creditsMenu;

    // Start is called before the first frame update
    void Start()
    {
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Final Scene");
    }

    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void ExitSettingsMenu()
    {
        settingsMenu.SetActive(false);
    }

    public void OpenCreditsMenu()
    {
        creditsMenu.SetActive(true);
    }

    public void ExitCreditsMenu()
    {
        creditsMenu.SetActive(false);
    }
}
