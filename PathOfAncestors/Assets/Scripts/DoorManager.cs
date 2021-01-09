using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : Activable
{
    private const string OPEN = "Open";
    [SerializeField]
    private List<MovableWallActivable> activables = new List<MovableWallActivable>();
  

    void Start()
    {
        AssociateActions();

    }

    public override void Activate()
    {
        if (!canActivate) return;

        if (_isActivated) return;

        foreach (MovableWallActivable part in activables)
        {
            part.Activate();
        }

        _isActivated = !_isActivated;
    }

    public override void Deactivate()
    {
        if (!canActivate) return;

        if (!_isActivated) return;

        foreach (MovableWallActivable part in activables)
        {
            part.Deactivate();
        }

        _isActivated = !_isActivated;
    }
}
