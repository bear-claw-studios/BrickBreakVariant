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
        Monetization.Initialize(iosID, true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)) {
            if(Monetization.IsReady(rewardedVideo)){
                ShowAdPlacementContent advert = null;
                advert = Monetization.GetPlacementContent(rewardedVideo) as ShowAdPlacementContent;

                if(advert != null){
                    advert.Show();
                }
            }
        }       
    }

    void WatchAdvert() {

    }

}
