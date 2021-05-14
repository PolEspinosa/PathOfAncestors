using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPuzzle1 : MonoBehaviour
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
            DataManager.puzzle1Deaths = DataManager.totalDeaths;
            DataManager.puzzle1TimePassed = DataManager.totalTimePassed;
            DataManager.puzzle1TimesActivated = DataManager.totalTimesActivated;
            DataManager.puzzle1TimesInteracted = DataManager.totalTimesInteracted;
            DataManager.puzzle1TimesOredredEarth = DataManager.totalTimesOrderedEarth;
            DataManager.puzzle1TimesOrderedFire = DataManager.totalTimesOrderedFire;
            DataManager.puzzle1TimesCorrectOrderedEarth = DataManager.totalTimesCorrectOrderedEarth;
            DataManager.puzzle1TimesCorrectOrderedFire = DataManager.totalTimesCorrectOrderedFire;
            DataManager.puzzle1TimesInvokedEarth = DataManager.totalTimesInvokedEarth;
            DataManager.puzzle1TimesInvokedFire = DataManager.totalTimesInvokedFire;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DataManager.SaveData();
            Destroy(gameObject);
        }
    }
}
