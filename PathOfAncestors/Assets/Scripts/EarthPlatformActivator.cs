using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPlatformActivator : Activator
{

    GameObject earthSpirit;
    public Transform endPos;
    
   
   

    private void OnTriggerEnter(Collider other)
    {
        if (!_activated)
        {
            if (other.tag == "EARTH")
            {
                if(order.isGoingToEarth && order.activator==this.gameObject)
                {
                    earthSpirit = other.gameObject;
                    earthSpirit.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                    _activated = true;
                    OnActivate();
                    manager.activatorObject = this;

                }


            }

        }
    }

    public void DeactivateEarthPlatform()
    {
        _activated = false;
        OnDeactivate();
        earthSpirit.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        manager.activatorObject = null;
        earthSpirit = null;
    }
}
