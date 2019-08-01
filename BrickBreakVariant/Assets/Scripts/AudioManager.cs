using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set; }
    public AudioSource music;
    public AudioSource rotator;
    public AudioSource fx;


    public bool playMusic = true;
    public bool playFx = true;
    public bool isRotating = false;


    public List<AudioClip> tracks = new List<AudioClip>();
    public AudioClip rumble;

    public AudioClip winFX;
    public AudioClip lossFX;
    public AudioClip changeColorFX;
    public AudioClip powerUpFX;
    public AudioClip extraLifeFX;


    private void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}

        tracks.Add(Resources.Load<AudioClip>("Audio/track1final"));
        tracks.Add(Resources.Load<AudioClip>("Audio/track2final"));
        tracks.Add(Resources.Load<AudioClip>("Audio/track3final"));
        tracks.Add(Resources.Load<AudioClip>("Audio/track4final"));

	}

    // Start is called before the first frame update
    void Start()
    {
        rotator.clip = rumble;
        // music = GetComponent<AudioSource>();
        if(playMusic){
            PickTrack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playMusic && !GameManager.Instance.isPaused && !music.isPlaying){
            PickTrack();
        }
        if(playFx && !GameManager.Instance.isPaused && isRotating && !rotator.isPlaying){
            rotator.Play();
        } else {
            rotator.Pause();
        }
    }

    void PickTrack(){
        int trackPick = Random.Range(0, 3);
        music.clip = tracks[trackPick];
        music.Play();
    }

    public void PlayEffect(string effect){
        if(playFx){
            switch(effect){
                case "win":
                    fx.clip = winFX;
                    break;
                case "loss":
                    fx.clip = lossFX;
                    break;
                case "powerup":
                    fx.clip = powerUpFX;
                    break;
                case "changeColor":
                    fx.clip = changeColorFX;
                    break;
                case "extraLife":
                    fx.clip = extraLifeFX;
                    break;
            }
            fx.Play();
        }  
    }

    public void ToggleMusic()
    {
        playMusic = !playMusic;
    }

    public void SetMusicVolume(float volume)
    {
        music.volume = volume;
    }

    public void ToggleSFX()
    {
        playFx = !playFx;
    }

    public void SetSFXVolume(float volume)
    {
        fx.volume = volume;
    }




    //rotator sound effect
    //ball and brick sound effect
    //music

}
