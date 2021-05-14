using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingObjectActivator : Activator
{
    public GameObject orb;
    // Start is called before the first frame update



    public void ActiveOrb()
    {
        if(!_activated)
        {
            DataManager.totalTimesActivated++;
            orb.SetActive(true);
            StartCoroutine(Active(2));
        }
        
    }

    IEnumerator Active(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _activated = true;
        OnActivate();
    }
   

}
