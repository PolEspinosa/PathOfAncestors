﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateDoorActivable : Activable
{

    public GameObject rightDoor;
    public GameObject leftDoor;

    public bool isRect = false;
    // Start is called before the first frame update
    void Start()
    {

        AssociateActions();
    }
    
    public override void Activate()
    {
        rightDoor.transform.DORotateQuaternion(Quaternion.Euler(0, 100, 0), 3f);
        leftDoor.transform.DORotateQuaternion(Quaternion.Euler(0, -100,0), 3f);

        if (isRect)
        {
            rightDoor.transform.DORotateQuaternion(Quaternion.Euler(0, -180, 0), 3f);
            leftDoor.transform.DORotateQuaternion(Quaternion.Euler(0, 10, 0), 3f);
        }
       
    }

    public override void Deactivate()
    {

        



    }
    
}
