using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Activable : MonoBehaviour
{
    [SerializeField] private Activator _activator = null;
    protected bool _isActivated;
    protected bool canActivate = true;

    protected void AssociateActions()
    {
        if (_activator != null)
        {
            _activator.OnActivate += Activate;
            _activator.OnActivate += AddStep;
            _activator.OnDeactivate += Deactivate;
        }
        else
        {
            Debug.LogWarning("Activable doesnt Have Activator activable is: " + this.gameObject.name);
        }
    }

   
    public abstract void Activate();

    public abstract void Deactivate();

    public virtual void AddStep()
    {
        Debug.Log("activated");
    } 

    public void SetActivator(Activator act)
    {
        _activator = act;
    }

    //its meant to be used after normal Activate and Deactivate Commands
    public void ToggleCanActivate(bool toggle)
    {
        canActivate = toggle;
    }

    public Activator Getactivator() => _activator;

    public bool GetActive() => _isActivated;
}
