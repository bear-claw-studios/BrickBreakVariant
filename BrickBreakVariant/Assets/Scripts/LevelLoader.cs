using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance {get; private set; }

    string[] bricks = {
                        "N11", "N12", "N13", "N14", "N15", "N16",
                        "N21", "N22", "N23", "N24", "N25", "N26", "N27", "N28",
                        "N31", "N32", "N33", "N34", "N35", "N36", "N37", "N38", "N39",
                        "N41", "N42", "N43", "N44", "N45", "N46", "N47", "N48", "N49", "N410",
                        "N51", "N52", "N53", "N54", "N55", "N56", "N57", "N58", "N59", "N510", "N511", "N512",
                        "N61", "N62", "N63", "N64", "N65", "N66", "N67", "N68", "N69", "N610", "N611", "N612", "N613", "N614", "N615",
                        "R11", "R12", "R13", "R14", "R15", "R16",
                        "R21", "R22", "R23", "R24", "R25", "R26", "R27", "R28",
                        "R31", "R32", "R33", "R34", "R35", "R36", "R37", "R38", "R39",
                        "R41", "R42", "R43", "R44", "R45", "R46", "R47", "R48", "R49", "R410",
                        "R51", "R52", "R53", "R54", "R55", "R56", "R57", "R58", "R59", "R510", "R511", "R512",
                        "R61", "R62", "R63", "R64", "R65", "R66", "R67", "R68", "R69", "R610", "R611", "R612", "R613", "R614", "R615",                        
                        };
    string[][] levelOne = {
        new string[] {"N11","N22","N33"}, //isFade
        new string[] {"N12","N13","N14"}, //isMatch-Black
        new string[] {"N15","N16"}, //isMatch-White
        new string[] {"N41","N42","N44", "N11","N22","N33"}, //toughness 0
        new string[] {"N51","N22"}, //toughness 1
        new string[] {"R61"} //toughness 2
    };

    //for each element in brick array check if exists in level array
    //search for game object with [element] name
    //set game object to active
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
        LoadLevel(one);
        UIManager.Instance.Notify("LEVEL ONE", 1f);
        UIManager.Instance.Subtitle("The Basics", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            LoadLevel(one);        
    }

    public void LoadLevel(string[][] level){
        foreach(string element in bricks){
            GameObject brick;
            brick = GameObject.Find(element);
            BrickController controller = brick.GetComponent<BrickController>();
            //isFade
            if(Array.Exists(level[0], el => el == element)){    
                controller.isActive = true;
                controller.isFade = true;
            }
            //isMatch-Black
            if(Array.Exists(level[1], el => el == element)){
                controller.isActive = true;
                controller.isMatch = true;
                controller.blue = true;
                controller.green = false;

            }
            //isMatch-White
            if(Array.Exists(level[2], el => el == element)){
                controller.isActive = true;
                controller.isMatch = true;
                controller.blue = false;
                controller.green = true;
            }
            //toughness 0
            if(Array.Exists(level[3], el => el == element)){    
                controller.isActive = true;
                controller.toughness = 0;
                GameManager.Instance.bricksLeft++;
            }
            //toughness 1
            if(Array.Exists(level[4], el => el == element)){    
                controller.isActive = true;
                controller.toughness = 1;
                GameManager.Instance.bricksLeft++;
            }
            //toughness 2
            if(Array.Exists(level[5], el => el == element)){    
                controller.isActive = true;
                controller.toughness = 2;
                GameManager.Instance.bricksLeft++;
            }

        }         
    }
    //LEVELS
    public string[][] one = {
        new string[] {}, //isFade
        new string[] {}, //isMatch-Black
        new string[] {}, //isMatch-White
        new string[] {"N11", "N12", "N13", "N14", "N15", "N16", 
                      "N31", "N32", "N33", "N34", "N35", "N36", "N37", "N38", "N39",
                      "N51", "N53", "N55", "N57", "N59", "N511"}, //toughness 0
        new string[] {}, //toughness 1
        new string[] {} //toughness 2
    };

    public string[][] two = {
        new string[] {}, //isFade
        new string[] {}, //isMatch-Black
        new string[] {}, //isMatch-White
        new string[] {"N31", "N33", "N35", "N37", "N39",
                      "N51", "N53", "N55", "N57", "N59", "N511"}, //toughness 0
        new string[] {"N11", "N12", "N13", "N14", "N15", "N16",}, //toughness 1
        new string[] {"N32", "N34", "N36", "N38"} //toughness 2
    };

}
