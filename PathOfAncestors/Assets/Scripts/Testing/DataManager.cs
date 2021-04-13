﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private const string SAVE_SEPARATOR = " ";

    //puzzle 1 data
    public float puzzle1TimePassed, puzzle1Deaths, puzzle1TimesOrdered, puzzle1TimesInvoked, puzzle1TimesPicked, puzzle1TimesMoved, puzzle1Steps;
    public float puzzle2TimePassed, puzzle2Deaths, puzzle2TimesOrdered, puzzle2TimesInvoked, puzzle2TimesPicked, puzzle2TimesMoved, puzzle2Steps;
    public float puzzle3TimePassed, puzzle3Deaths, puzzle3TimesOrdered, puzzle3TimesInvoked, puzzle3TimesPicked, puzzle3TimesMoved, puzzle3Steps;

    

    // Start is called before the first frame update
    void Start()
    {
        //puzzle1TimePassed = puzzle2TimePassed = puzzle1Steps = puzzle2Steps = puzzle1Deaths = puzzle2Deaths = 0;
    }

    // Update is called once per frame
    void Update()
    {

        puzzle1Steps = puzzle1TimesOrdered + puzzle1TimesInvoked + puzzle1TimesPicked + puzzle1TimesMoved;
        //Debug.Log("Puzzle 1 deaths: " + puzzle1Deaths);
        
        puzzle2Steps = puzzle2TimesOrdered + puzzle2TimesInvoked + puzzle2TimesPicked + puzzle2TimesMoved;
        //Debug.Log("Puzzle 2 deaths: " + puzzle2Deaths);
    }

    public void SaveData()
    {
        string[] dataToSave = new string[]
        {
            "puzzle1Steps " + puzzle1Steps,
            "puzzle1TimePassed " + puzzle1TimePassed,
            "puzzle1Deaths " + puzzle1Deaths,
            "puzzle2Steps " + puzzle2Steps,
            "puzzle2TimePassed " + puzzle2TimePassed,
            "puzzle2Deaths " + puzzle2Deaths
        };

        string saveString = string.Join(SAVE_SEPARATOR, dataToSave);
        File.WriteAllText(Application.streamingAssetsPath + "/data.txt", saveString);
    }
}
