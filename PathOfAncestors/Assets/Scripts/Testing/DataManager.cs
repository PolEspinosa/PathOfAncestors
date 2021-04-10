using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //puzzle 1 data
    public float puzzle1Time, puzzle1Deaths, puzzle1TimesOrdered, puzzle1TimesInvoked, puzzle1TimesPicked, puzzle1TimesMoved, puzzle1Steps;
    public float puzzle2Time, puzzle2Deaths, puzzle2TimesOrdered, puzzle2TimesInvoked, puzzle2TimesPicked, puzzle2TimesMoved, puzzle2Steps;
    public float puzzle3Time, puzzle3Deaths, puzzle3TimesOrdered, puzzle3TimesInvoked, puzzle3TimesPicked, puzzle3TimesMoved, puzzle3Steps;

    // Start is called before the first frame update
    void Start()
    {
        puzzle1Time = puzzle2Time = puzzle1Steps = puzzle2Steps = puzzle1Deaths = puzzle2Deaths = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //puzzle1Steps = puzzle1TimesOrdered + puzzle1TimesInvoked + puzzle1TimesPicked + puzzle1TimesMoved;
        //Debug.Log("Puzzle 1 steps: " + puzzle1Steps);
        //
        //puzzle2Steps = puzzle2TimesOrdered + puzzle2TimesInvoked + puzzle2TimesPicked + puzzle2TimesMoved;
        //Debug.Log("Puzzle 2 steps: " + puzzle2Steps);
    }
}
