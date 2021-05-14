using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPuzzle1 : MonoBehaviour
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
            DataManager.totalDeaths = 0;
            DataManager.totalTimePassed = 0;
            DataManager.totalTimesActivated = 0;
            DataManager.totalTimesInteracted = 0;
            DataManager.totalTimesOrderedEarth = 0;
            DataManager.totalTimesOrderedFire = 0;
            DataManager.totalTimesInvokedEarth = 0;
            DataManager.totalTimesInvokedFire = 0;
            DataManager.totalTimesCorrectOrderedEarth = 0;
            DataManager.totalTimesCorrectOrderedFire = 0;
            DataManager.totalTimesNOTCorrectOrderedEarth = 0;
            DataManager.totalTimesNOTCorrectOrderedFire = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
