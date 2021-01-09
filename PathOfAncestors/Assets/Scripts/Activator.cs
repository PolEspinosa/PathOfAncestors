using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class Activator : MonoBehaviour
{
   

    public bool _activated;

    public bool fromAGroup = false;

    protected bool groupActivated = false;
    public SpiritManager manager;

    public Action OnActivate = delegate { };
    public Action OnDeactivate = delegate { };

    private void Start()
    {
        manager = GameObject.Find("Character").GetComponent<SpiritManager>();
    }
    public virtual void GroupActivated()
    {

    }
    protected virtual void Activate()
    {

    }

  
}
