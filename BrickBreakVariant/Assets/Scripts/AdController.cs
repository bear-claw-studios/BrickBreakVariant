using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdController : MonoBehaviour
{
    public static AdController Instance {get; private set; }

    private string iosID = "3240299";
    private string androidID = "3240298";
    private string rewardedVideo = "rewardedVideo";

	private void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        //set to false for deployed build
        if(Monetization.isSupported){
            Monetization.Initialize(iosID, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) {
            WatchAdvert();
        }       
    }

    public void WatchAdvert() {
        AudioManager.Instance.AdBreak("start");
        GameManager.Instance.adPlaying = true;
        GameManager.Instance.isExtra = true;
        if(Monetization.IsReady(rewardedVideo)){

            ShowAdCallbacks options = new ShowAdCallbacks ();
            options.finishCallback = HandleShowResult;

            ShowAdPlacementContent advert = null;
            advert = Monetization.GetPlacementContent(rewardedVideo) as ShowAdPlacementContent;

            if(advert != null){
                advert.Show(options);
            }
        }
    }

    void HandleShowResult (ShowResult result) {
        if (result == ShowResult.Finished) {
            // Reward the player
            GameManager.Instance.lives++;
            UIManager.Instance.gameOverMenu.SetActive(false);
            UIManager.Instance.Resume();
            GameManager.Instance.activeGame = true;
            AudioManager.Instance.AdBreak("end");
            GameManager.Instance.adPlaying = false;

            //Exit button was being clicked while the Ad close button was clicked too
            UIManager.Instance.exitButton.enabled = true;

        } else if (result == ShowResult.Skipped) {
            Debug.LogWarning ("The player skipped the video - DO NOT REWARD!");
        } else if (result == ShowResult.Failed) {
            Debug.LogError ("Video failed to show");
        }
    }
}
