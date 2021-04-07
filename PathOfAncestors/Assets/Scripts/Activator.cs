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
    //create the event instance for the pressure plate activate sound
    protected FMOD.Studio.EventInstance pressurePlateActivateSoundInstance;
    //create the event instance for the pressure plate deactivate sound
    protected FMOD.Studio.EventInstance pressurePlateDeactivateSoundInstance;

    protected virtual void Start()
    {
        manager = GameObject.Find("Character").GetComponent<SpiritManager>();
        order = GameObject.Find("Character").GetComponent<OrderSystem>();

        ovenSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Props/activateOven");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(ovenSoundInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
        torchSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Props/activateFire");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(torchSoundInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
        pressurePlateActivateSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Mecanismos/activatePressurePlate");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pressurePlateActivateSoundInstance, gameObject.transform, gameObject.GetComponentInChildren<Rigidbody>());
        pressurePlateDeactivateSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Mecanismos/deactivatePressurePlate");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pressurePlateDeactivateSoundInstance, gameObject.transform, gameObject.GetComponentInChildren<Rigidbody>());
    }
    public virtual void GroupActivated()
    {

    }
    protected virtual void Activate()
    {

    }

  
}
