using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateActivator : Activator
{

    public float pressedPos;
    public Vector3 startPos;
    public GameObject pressPlateChild;


    protected override void Start()
    {
        base.Start();
        startPos = pressPlateChild.transform.localPosition;
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
    }
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
