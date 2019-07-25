using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set; }
    
    //Add Ball
    public Transform ball;

	private void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}


    public void AddBall () {
        Instantiate(ball, GameManager.Instance.ballVector, Quaternion.identity);
    }
}
