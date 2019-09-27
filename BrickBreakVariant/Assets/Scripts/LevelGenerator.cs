using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
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

    void GenerateLevel(){
        foreach(string element in bricks){
            GameObject brick;
            brick = GameObject.Find(element);
            BrickController controller = brick.GetComponent<BrickController>();
            if(Random.Range(0, 3) == 0){
                controller.isActive = true;
                GameManager.Instance.bricksLeft++;
                int type = Random.Range(0, 3);
                switch(type){
                    case 0: //isFade
                        controller.isFade = true;
                        controller.toughness = Random.Range(0, 3);
                        break;
                    case 1: //isMatch
                        controller.isMatch = true;
                        controller.toughness = 0;
                        if(Random.Range(0,2) == 0){
                            controller.blue = true;
                            controller.green = false;
                        } else {
                            controller.blue = false;
                            controller.green = true;
                        }
                        break;
                    case 2: //just tough
                        //maybe switch to different odds later
                        controller.toughness = Random.Range(0, 3);
                        break;
                }
            }
        }
    }
}
