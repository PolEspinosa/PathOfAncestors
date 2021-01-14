using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateActivator : Activator
{
    private void OnTriggerEnter(Collider other)
    {
        if (!_activated)
        {
            if (other.tag=="Player" || other.tag == "EARTH")
            {
                _activated = true;
                OnActivate();
                if(other.tag=="EARTH")
                {
                    manager.activatorObject = this;
                }
                
            }
       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_activated)
        {
            _activated = false;
            OnDeactivate();
            manager.activatorObject = null;
        }
    }
}
