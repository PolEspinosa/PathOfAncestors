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

       
    }
    private void OnTriggerEnter(Collider other)
    {
        colliders.Add(other.transform.gameObject);
        if (!_activated)
        {
            if (other.tag=="Player2" || other.tag == "EARTH" || other.tag=="EarthPlatform")
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
        colliders.Remove(other.transform.gameObject);
        if (_activated && colliders.Count<=0)
        {
            _activated = false;
            OnDeactivate();
            
        }
        if (other.tag == "EARTH")
        {
            manager.activatorObject = null;
        }

    }
}
