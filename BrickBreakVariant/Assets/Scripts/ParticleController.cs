using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void ChangeParticle(string color){
        if(color == "blue"){
            particle.startColor = Color.blue;
        } else if (color == "green"){
            particle.startColor = Color.green;
        }
    }

    // public void TriggerSpark(string color){
    //     if(color == "blue"){
    //         particle.startColor = Color.blue;
    //     } else if (color == "green"){
    //         particle.startColor = Color.green;
    //     } else if (color == "red"){
    //         particle.startColor = Color.red;
    //     } else if (color == "yellow"){
    //         particle.startColor = Color.yellow;
    //     } else if (color == "orange"){
    //         particle.startColor = new Color(1f, 0.64f, 0f, 1f);
    //     }
    //     particle.Play();
    // }

}
