using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchActivator : Activator

{
    private void OnTriggerEnter(Collider other)
    {
        if (!_activated)
        {
            if (other.tag == "FIRE" )
            {
                _activated = true;
                OnActivate();
                //start the fire sound when the torch is activated
                //torchSoundInstance.start();
            }

        }
    }

   
}
