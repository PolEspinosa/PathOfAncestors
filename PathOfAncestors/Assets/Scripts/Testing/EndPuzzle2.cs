using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPuzzle2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && DataManager.objectPicked)
        {
            DataManager.puzzle2Deaths = DataManager.totalDeaths;
            DataManager.puzzle2TimePassed = DataManager.totalTimePassed;
            DataManager.puzzle2TimesActivated = DataManager.totalTimesActivated;
            DataManager.puzzle2TimesInteracted = DataManager.totalTimesInteracted;
            DataManager.puzzle2TimesOredredEarth = DataManager.totalTimesOrderedEarth;
            DataManager.puzzle2TimesOrderedFire = DataManager.totalTimesOrderedFire;
            DataManager.puzzle2TimesInvokedEarth = DataManager.totalTimesInvokedEarth;
            DataManager.puzzle2TimesInvokedFire = DataManager.totalTimesInvokedFire;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && DataManager.objectPicked)
        {
            DataManager.SaveData2();
            Destroy(gameObject);
        }
    }
}
