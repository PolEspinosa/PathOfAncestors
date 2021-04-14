using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    private const string SAVE_SEPARATOR = " ";

    //puzzle 1 data
    public static float puzzle1TimePassed, puzzle1Deaths, puzzle1TimesOrderedFire, puzzle1TimesOredredEarth, puzzle1TimesInvokedFire; 
    public static float puzzle1TimesInvokedEarth, puzzle1TimesActivated, puzzle1TimesInteracted, puzzle1Steps;
    //puzzle 2 data
    public static float puzzle2TimePassed, puzzle2Deaths, puzzle2TimesOrderedFire, puzzle2TimesOredredEarth, puzzle2TimesInvokedFire;
    public static float puzzle2TimesInvokedEarth, puzzle2TimesActivated, puzzle2TimesInteracted, puzzle2Steps;
    //only for puzzle 2
    public static bool objectPicked;
    //puzzle 3 data
    public static float puzzle3TimePassed, puzzle3Deaths, puzzle3TimesOrderedFire, puzzle3TimesOredredEarth, puzzle3TimesInvokedFire;
    public static float puzzle3TimesInvokedEarth, puzzle3TimesActivated, puzzle3TimesInteracted, puzzle3Steps;
    //puzzle 4 data
    public static float puzzle4TimePassed, puzzle4Deaths, puzzle4TimesOrderedFire, puzzle4TimesOredredEarth, puzzle4TimesInvokedFire;
    public static float puzzle4TimesInvokedEarth, puzzle4TimesActivated, puzzle4TimesInteracted, puzzle4Steps;
    //puzzle 5 data
    public static float puzzle5TimePassed, puzzle5Deaths, puzzle5TimesOrderedFire, puzzle5TimesOredredEarth, puzzle5TimesInvokedFire;
    public static float puzzle5TimesInvokedEarth, puzzle5TimesActivated, puzzle5TimesInteracted, puzzle5Steps;

    //variables to store global data
    public static float totalTimePassed, totalDeaths, totalTimesInvokedFire, totalTimesInvokedEarth, totalTimesOrderedFire, totalTimesOrderedEarth;
    public static float totalTimesActivated, totalTimesInteracted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get the steps of puzzle 1
        puzzle1Steps = puzzle1TimesOrderedFire + puzzle1TimesOredredEarth + puzzle1TimesInvokedFire + puzzle1TimesInvokedEarth
            + puzzle1TimesActivated + puzzle1TimesInteracted;
        //get the steps of puzzle 2
        puzzle2Steps = puzzle2TimesOrderedFire + puzzle2TimesOredredEarth + puzzle2TimesInvokedFire + puzzle2TimesInvokedEarth
            + puzzle2TimesActivated + puzzle2TimesInteracted;
        //get the steps of puzzle 3
        puzzle3Steps = puzzle3TimesOrderedFire + puzzle3TimesOredredEarth + puzzle3TimesInvokedFire + puzzle3TimesInvokedEarth
            + puzzle3TimesActivated + puzzle3TimesInteracted;
        //get the steps of puzzle 4
        puzzle4Steps = puzzle4TimesOrderedFire + puzzle4TimesOredredEarth + puzzle4TimesInvokedFire + puzzle4TimesInvokedEarth
            + puzzle4TimesActivated + puzzle4TimesInteracted;
        //get the steps of puzzle 25
        puzzle5Steps = puzzle5TimesOrderedFire + puzzle5TimesOredredEarth + puzzle5TimesInvokedFire + puzzle5TimesInvokedEarth
            + puzzle5TimesActivated + puzzle5TimesInteracted;
    }

    public static void SaveData()
    {
        string[] dataToSave = new string[]
        {
            "puzzle1TimePassed: " + puzzle1TimePassed,
            "puzzle1Deaths: " + puzzle1Deaths,
            "puzzle1TimesOrderedFire: " + puzzle1TimesOrderedFire,
            "puzzle1TimesOredredEarth: " + puzzle1TimesOredredEarth,
            "puzzle1TimesInvokedFire: " + puzzle1TimesInvokedFire,
            "puzzle1TimesInvokedEarth: " + puzzle1TimesInvokedEarth,
            "puzzle1TimesActivated: " + puzzle1TimesActivated,
            "puzzle1TimesInteracted: " + puzzle1TimesInteracted,
            "puzzle1Steps: " + puzzle1Steps
        };

        string saveString = string.Join(SAVE_SEPARATOR, dataToSave);
        File.WriteAllText(Application.streamingAssetsPath + "/data.txt", saveString);
    }

    public static void SaveData2()
    {
        string[] dataToSave = new string[]
        {
            "puzzle2TimePassed: " + puzzle2TimePassed,
            "puzzle2Deaths: " + puzzle2Deaths,
            "puzzle2TimesOrderedFire: " + puzzle2TimesOrderedFire,
            "puzzle2TimesOredredEarth: " + puzzle2TimesOredredEarth,
            "puzzle2TimesInvokedFire: " + puzzle2TimesInvokedFire,
            "puzzle2TimesInvokedEarth: " + puzzle2TimesInvokedEarth,
            "puzzle2TimesActivated: " + puzzle2TimesActivated,
            "puzzle2TimesInteracted: " + puzzle2TimesInteracted,
            "puzzle2Steps: " + puzzle2Steps
        };

        string saveString = string.Join(SAVE_SEPARATOR, dataToSave);
        File.WriteAllText(Application.streamingAssetsPath + "/data2.txt", saveString);
    }

    public static void SaveData3()
    {
        string[] dataToSave = new string[]
        {
            "puzzle3TimePassed: " + puzzle3TimePassed,
            "puzzle3Deaths: " + puzzle3Deaths,
            "puzzle3TimesOrderedFire: " + puzzle3TimesOrderedFire,
            "puzzle3TimesOredredEarth: " + puzzle3TimesOredredEarth,
            "puzzle3TimesInvokedFire: " + puzzle3TimesInvokedFire,
            "puzzle3TimesInvokedEarth: " + puzzle3TimesInvokedEarth,
            "puzzle3TimesActivated: " + puzzle3TimesActivated,
            "puzzle3TimesInteracted: " + puzzle3TimesInteracted,
            "puzzle3Steps: " + puzzle3Steps
        };

        string saveString = string.Join(SAVE_SEPARATOR, dataToSave);
        File.WriteAllText(Application.streamingAssetsPath + "/data3.txt", saveString);
    }

    public static void SaveData4()
    {
        string[] dataToSave = new string[]
        {
            "puzzle4TimePassed: " + puzzle4TimePassed,
            "puzzle4Deaths: " + puzzle4Deaths,
            "puzzle4TimesOrderedFire: " + puzzle4TimesOrderedFire,
            "puzzle4TimesOredredEarth: " + puzzle4TimesOredredEarth,
            "puzzle4TimesInvokedFire: " + puzzle4TimesInvokedFire,
            "puzzle4TimesInvokedEarth: " + puzzle4TimesInvokedEarth,
            "puzzle4TimesActivated: " + puzzle4TimesActivated,
            "puzzle4TimesInteracted: " + puzzle4TimesInteracted,
            "puzzle4Steps: " + puzzle4Steps
        };

        string saveString = string.Join(SAVE_SEPARATOR, dataToSave);
        File.WriteAllText(Application.streamingAssetsPath + "/data4.txt", saveString);
    }

    public static void SaveData5()
    {
        string[] dataToSave = new string[]
        {
            "puzzle5TimePassed: " + puzzle5TimePassed,
            "puzzle5Deaths: " + puzzle5Deaths,
            "puzzle5TimesOrderedFire: " + puzzle5TimesOrderedFire,
            "puzzle5TimesOredredEarth: " + puzzle5TimesOredredEarth,
            "puzzle5TimesInvokedFire: " + puzzle5TimesInvokedFire,
            "puzzle5TimesInvokedEarth: " + puzzle5TimesInvokedEarth,
            "puzzle5TimesActivated: " + puzzle5TimesActivated,
            "puzzle5TimesInteracted: " + puzzle5TimesInteracted,
            "puzzle5Steps: " + puzzle5Steps
        };

        string saveString = string.Join(SAVE_SEPARATOR, dataToSave);
        File.WriteAllText(Application.streamingAssetsPath + "/data5.txt", saveString);
    }
}
