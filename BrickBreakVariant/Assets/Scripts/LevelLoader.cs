using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelLoader : MonoBehaviour
{
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
    string[] level = {};

    //for each element in brick array check if exists in level array
    //search for game object with [element] name
    //set game object to active

    // Start is called before the first frame update
    void Start()
    {
        foreach(string element in bricks){
            if(Array.Exists(level, el => el == element)){
                
            }    
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
