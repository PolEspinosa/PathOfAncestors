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

    //create the event instance for the oven sound
    protected FMOD.Studio.EventInstance ovenSoundInstance;
    //create the event instance for the torch sound
    protected FMOD.Studio.EventInstance torchSoundInstance;

    protected virtual void Start()
    {
        manager = GameObject.Find("Character").GetComponent<SpiritManager>();
        order = GameObject.Find("Character").GetComponent<OrderSystem>();

        //ovenSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Props/activateOven");
        //torchSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Props/activateFire");
    }
    public virtual void GroupActivated()
    {

    }
    protected virtual void Activate()
    {

    }

  
}
