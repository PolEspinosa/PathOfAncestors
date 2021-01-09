using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateDoorActivable : Activable
{

    public GameObject rightDoor;
    public GameObject leftDoor;
    // Start is called before the first frame update
    void Start()
    {

        AssociateActions();
    }
    
    public override void Activate()
    {
        rightDoor.transform.DORotateQuaternion(Quaternion.Euler(0, -45, 0), 3f);
        leftDoor.transform.DORotateQuaternion(Quaternion.Euler(0, 45, 0), 3f);
       
    }

    public override void Deactivate()
    {

        



    }
    
}
