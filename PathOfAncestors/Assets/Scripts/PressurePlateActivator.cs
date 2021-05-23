using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateActivator : Activator
{

    public float pressedPos;
    public Vector3 startPos;
    public GameObject pressPlateChild;
    public List<GameObject> colliders;

    protected override void Start()
    {
        base.Start();
        startPos = pressPlateChild.transform.localPosition;
        colliders = new List<GameObject>();
    }

    private void Update()
    {
        if (_activated)
        {
            if (pressPlateChild.transform.localPosition.y > pressedPos)
                pressPlateChild.transform.localPosition -= Vector3.up * 0.01f;
            else if (transform.localPosition.y < pressedPos)
                pressPlateChild.transform.localPosition = Vector3.up * pressedPos;
        }
        else if (!_activated)
        {
            if (pressPlateChild.transform.localPosition.y < startPos.y)
                pressPlateChild.transform.localPosition += Vector3.up * 0.01f;

        }

        if(colliders.Count<=0 && _activated)
        {
            _activated = false;
            OnDeactivate();
        }

        if(_activated)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                if (colliders[i] == null)
                {
                    colliders.RemoveAt(i);
                }
            }
        }
    

       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.name != "Trigger" && other.transform.gameObject.name != "EarthInvokation")
        {
            colliders.Add(other.transform.gameObject);
        }
            
        if (!_activated)
        {
            if (other.tag=="Player" || other.tag == "EARTH" || other.tag=="EarthPlatform")
            {
                //play pressure plate activate sound
                pressurePlateActivateSoundInstance.start();
                _activated = true;
                OnActivate();
                if(other.tag=="EARTH")
                {
                    manager.activatorObject = this;
                    other.gameObject.GetComponent<EarthSpirit>().onPressurePlate = true;
                }
                
            }
       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other.transform.gameObject);
        if (_activated && colliders.Count<=0)
        {
            _activated = false;
            OnDeactivate();
            //play pressure deactivate plate sound
            pressurePlateActivateSoundInstance.start();
        }
        if (other.tag == "EARTH")
        {
            manager.activatorObject = null;
            //play pressure deactivate plate sound
            pressurePlateActivateSoundInstance.start();
            other.gameObject.GetComponent<EarthSpirit>().onPressurePlate = false;
        }

    }
}
