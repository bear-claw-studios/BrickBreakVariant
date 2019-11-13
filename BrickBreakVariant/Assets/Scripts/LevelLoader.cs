using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance {get; private set; }

    public bool isLoaded = false;

    string[] bricks = {
                        "N11", "N12", "N13", "N14", "N15", "N16", "N17", "N18",
                        "N21", "N22", "N23", "N24", "N25", "N26", "N27", "N28", "N29", "N210",
                        "N31", "N32", "N33", "N34", "N35", "N36", "N37", "N38", "N39", "N310", "N311", "N312",
                        "N41", "N42", "N43", "N44", "N45", "N46", "N47", "N48", "N49", "N410", "N411", "N412", "N413", "N414", "N415", "N416",
                        "N51", "N52", "N53", "N54", "N55", "N56", "N57", "N58", "N59", "N510", "N511", "N512", "N513", "N514", "N515", "N516", "N517", "N518",
                        "N61", "N62", "N63", "N64", "N65", "N66", "N67", "N68", "N69", "N610", "N611", "N612", "N613", "N614", "N615", "N616", "N617", "N618", "N619", "N620",
                        "R11", "R12", "R13", "R14", "R15", "R16", "R17", "R18",
                        "R21", "R22", "R23", "R24", "R25", "R26", "R27", "R28", "R29", "R210",
                        "R31", "R32", "R33", "R34", "R35", "R36", "R37", "R38", "R39", "R310", "R311", "R312",
                        "R41", "R42", "R43", "R44", "R45", "R46", "R47", "R48", "R49", "R410", "R411", "R412", "R413", "R414", "R415", "R416",
                        "R51", "R52", "R53", "R54", "R55", "R56", "R57", "R58", "R59", "R510", "R511", "R512", "R513", "R514", "R515", "R516", "R517", "R518",
                        "R61", "R62", "R63", "R64", "R65", "R66", "R67", "R68", "R69", "R610", "R611", "R612", "R613", "R614", "R615", "R616", "R617", "R618", "R619", "R620",                        
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
        // LoadLevel(one);
        // UIManager.Instance.Notify("LEVEL ONE", 1f);
        // UIManager.Instance.Subtitle("The Basics", 1f);
        // GenerateLevel();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            LoadLevel(one);        
    }

    public void LoadLevel(string[][] level){
        ResetBricks();
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
                controller.toughness = 0;
                GameManager.Instance.bricksLeft++;

            }
            //isMatch-White
            if(Array.Exists(level[2], el => el == element)){
                controller.isActive = true;
                controller.isMatch = true;
                controller.blue = false;
                controller.green = true;
                controller.toughness = 0;
                GameManager.Instance.bricksLeft++;
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
        // StartCoroutine(GameController.Instance.Respawn());
        isLoaded = true;         
    }

    public void GenerateLevel(){
        ResetBricks();
        for(var i = 0; i < bricks.Length; i++){
                GameObject brick;
                brick = GameObject.Find(bricks[i]);
                BrickController controller = brick.GetComponent<BrickController>();
            if(i < 6){
                controller.isActive = true;
                GameManager.Instance.bricksLeft++;
                controller.toughness = UnityEngine.Random.Range(0, 3);
            } else {
                if(UnityEngine.Random.Range(0, 3) == 0){
                    controller.isActive = true;
                    GameManager.Instance.bricksLeft++;
                    int type = UnityEngine.Random.Range(0, 3);
                    switch(type){
                        case 0: //isFade
                            controller.isFade = true;
                            controller.toughness = UnityEngine.Random.Range(0, 3);
                            break;
                        case 1: //isMatch
                            controller.isMatch = true;
                            controller.toughness = 0;
                            if(UnityEngine.Random.Range(0,2) == 0){
                                controller.blue = true;
                                controller.green = false;
                            } else {
                                controller.blue = false;
                                controller.green = true;
                            }
                            break;
                        case 2: //just tough
                            //maybe switch to different odds later
                            controller.toughness = UnityEngine.Random.Range(0, 3);
                            break;
                    }
                }                
            }
		}
        isLoaded = true;
    }

    public void ResetBricks(){
        for(var i = 0; i < bricks.Length; i++){
            GameObject brick;
            brick = GameObject.Find(bricks[i]);
            BrickController controller = brick.GetComponent<BrickController>();
            controller.isActive = false;
            controller.isFade = false;
            controller.isMatch = false;
            controller.toughness = 0;
        }
    }

    //LEVELS
    //Match bricks set to toughness one automatically and should not be included in toughness lists.
    //Fade bricks must have a toughness declared or also be marked as fade.
    public string[][] one = {
        new string[] {}, //isFade
        new string[] {}, //isMatch-Black
        new string[] {}, //isMatch-White
        new string[] {"N11", "N12", "N13", "N14", "N15", "N16", "N17", "N18",
                      "N31", "N32", "N33", "N34", "N35", "N36", "N37", "N38", "N39", "N310", "N311", "N312",
                      "N51", "N53", "N55", "N57", "N59", "N511", "N513", "N515", "N517"}, //toughness 0
        new string[] {}, //toughness 1
        new string[] {} //toughness 2
    };

    public string[][] two = {
        new string[] {}, //isFade
        new string[] {}, //isMatch-Black
        new string[] {}, //isMatch-White
        new string[] {"N31", "N32", "N33", "N34", "N35", "N36", "N37", "N38", "N39", "N310", "N311", "N312",
                      "N51", "N57", "N513"}, //toughness 0
        new string[] {"N11", "N13", "N15", "N17",
                      "N53", "N59", "N515"}, //toughness 1
        new string[] {"N12", "N14", "N16", "N18",
                      "N55", "N511", "N517"} //toughness 2
    };
       
    public string[][] three = {
        new string[] {}, //isFade
        new string[] {}, //isMatch-Black
        new string[] {}, //isMatch-White
        new string[] {"N11", "N12", "N15", "N16",
                      "N61", "N62", "N65", "N66", "N69", "N610", "N613", "N614", "N617", "N618",
                      "R41", "R43", "R45", "R47", "R49", "R411", "R413", "R415"}, //toughness 0
        new string[] {"R42", "R44", "R46", "R48", "R410", "R412", "R414", "R416"}, //toughness 1
        new string[] {"N13", "N14", "N17", "N18"} //toughness 2
    };

    public string[][] four = {
        new string[] {}, //isFade
        new string[] {}, //isMatch-Black
        new string[] {}, //isMatch-White
        new string[] {"N11", "N13", "N15", "N17",
                      "N21", "N23", "N25", "N27", "N29",
                      "R12", "R14", "R16", "R18"}, //toughness 0
        new string[] {"R22", "R24", "R26", "R28", "R210"}, //toughness 1
        new string[] {"N51", "N55", "N59", "N513", "N517",
                      "R53", "R57", "R511", "R515"} //toughness 2
    };
    public string[][] five = {
        new string[] {"N21", "N24", "N25", "N28",
                      "N32", "N36", "N37", "N310",
                      "N43", "N44", "N49", "N410", "N414", "N415",
                      "N55", "N56", "N512", "N513", "N517", "N518",
                      "N61", "N62", "N63", "N67", "N68", "N69", "N615", "N616", "N617"}, //isFade
        new string[] {"R11", "R13", "R15", "R17"}, //isMatch-Black
        new string[] {"R12", "R14", "R16", "R18"}, //isMatch-White
        new string[] {"N21", "N24", "N25", "N28",
                      "N32", "N36", "N37", "N310",
                      "N43", "N44", "N49", "N410", "N414", "N415",
                      "N55", "N56", "N512", "N513", "N517", "N518",
                      "N61", "N62", "N63", "N67", "N68", "N69", "N615", "N616", "N617"}, //toughness 0
        new string[] {}, //toughness 1
        new string[] {} //toughness 2
    };

    public string[][] six = {
        new string[] {}, //isFade
        new string[] {"N31", "N32", "N33", "N34", "N35", "N36", "N37", "N38", "N39", "N310", "N311", "N312"}, //isMatch-Black
        new string[] {"R61", "R62", "R63", "R64", "R65", "R66", "R67", "R68", "R69", "R610", "R611", "R612", "R613", "R614", "R615", "R616", "R617", "R618", "R619", "R6 20"}, //isMatch-White
        new string[] {"R11", "R12", "R13", "R14"}, //toughness 0
        new string[] {"R15", "R16", "R17", "R18"}, //toughness 1
        new string[] {} //toughness 2
    };


}
