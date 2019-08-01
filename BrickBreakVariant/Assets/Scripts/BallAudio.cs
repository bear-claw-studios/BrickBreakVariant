using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bounce;
    public AudioClip brickBreak;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ballContact(string type){
        if(AudioManager.Instance.playFx){
            if(type == "bounce"){
                audioSource.clip = bounce;
            } else if(type == "break"){
                audioSource.clip = brickBreak;
            }
            audioSource.Play();
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
