using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPuzzle4 : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            DataManager.puzzle4Deaths = DataManager.totalDeaths;
            DataManager.puzzle4TimePassed = DataManager.totalTimePassed;
            DataManager.puzzle4TimesActivated = DataManager.totalTimesActivated;
            DataManager.puzzle4TimesInteracted = DataManager.totalTimesInteracted;
            DataManager.puzzle4TimesOredredEarth = DataManager.totalTimesOrderedEarth;
            DataManager.puzzle4TimesOrderedFire = DataManager.totalTimesOrderedFire;
            DataManager.puzzle4TimesInvokedEarth = DataManager.totalTimesInvokedEarth;
            DataManager.puzzle4TimesInvokedFire = DataManager.totalTimesInvokedFire;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DataManager.SaveData4();
            Destroy(gameObject);
        }
    }
}
