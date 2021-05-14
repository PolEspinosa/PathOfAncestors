using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPuzzle3 : MonoBehaviour
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
            DataManager.puzzle3Deaths = DataManager.totalDeaths;
            DataManager.puzzle3TimePassed = DataManager.totalTimePassed;
            DataManager.puzzle3TimesActivated = DataManager.totalTimesActivated;
            DataManager.puzzle3TimesInteracted = DataManager.totalTimesInteracted;
            DataManager.puzzle3TimesOredredEarth = DataManager.totalTimesOrderedEarth;
            DataManager.puzzle3TimesOrderedFire = DataManager.totalTimesOrderedFire;
            DataManager.puzzle3TimesInvokedEarth = DataManager.totalTimesInvokedEarth;
            DataManager.puzzle3TimesInvokedFire = DataManager.totalTimesInvokedFire;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DataManager.SaveData3();
            Destroy(gameObject);
        }
    }
}
