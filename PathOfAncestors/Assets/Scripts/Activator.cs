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
    public OrderSystem order;

    public Action OnActivate = delegate { };
    public Action OnDeactivate = delegate { };

    private void Start()
    {
        manager = GameObject.Find("Character").GetComponent<SpiritManager>();
        order = GameObject.Find("Character").GetComponent<OrderSystem>();
    }
    public virtual void GroupActivated()
    {

    }
    protected virtual void Activate()
    {

    }

  
}
