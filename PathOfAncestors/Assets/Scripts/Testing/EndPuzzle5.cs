using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPuzzle5 : MonoBehaviour
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
            DataManager.puzzle5Deaths = DataManager.totalDeaths;
            DataManager.puzzle5TimePassed = DataManager.totalTimePassed;
            DataManager.puzzle5TimesActivated = DataManager.totalTimesActivated;
            DataManager.puzzle5TimesInteracted = DataManager.totalTimesInteracted;
            DataManager.puzzle5TimesOredredEarth = DataManager.totalTimesOrderedEarth;
            DataManager.puzzle5TimesOrderedFire = DataManager.totalTimesOrderedFire;
            DataManager.puzzle5TimesCorrectOrderedEarth = DataManager.totalTimesCorrectOrderedEarth;
            DataManager.puzzle5TimesCorrectOrderedFire = DataManager.totalTimesCorrectOrderedFire;
            DataManager.puzzle5TimesInvokedEarth = DataManager.totalTimesInvokedEarth;
            DataManager.puzzle5TimesInvokedFire = DataManager.totalTimesInvokedFire;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DataManager.SaveData5();
            Destroy(gameObject);
        }
    }
}
