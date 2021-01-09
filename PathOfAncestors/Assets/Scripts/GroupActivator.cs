using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupActivator : Activator
{

    [SerializeField]
    private List<Activator> activators = new List<Activator>();

    bool allActivated;

  

    // Update is called once per frame
    void Update()
    {
        if(!_activated)
        {
            checkActivators();
        }
        
    }

    void checkActivators()
    {
        bool aux = true;
        foreach (Activator activator in activators)
        {
            if (activator._activated)
                continue;
            else
                aux = false;
        }
       
        if(aux)
        {
            _activated = true;
            OnActivate();
        }
    }
}
