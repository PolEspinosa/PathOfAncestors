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
        rightDoor.transform.DORotateQuaternion(Quaternion.Euler(0, 100, 0), 3f);
        leftDoor.transform.DORotateQuaternion(Quaternion.Euler(0, -100,0), 3f);
        //play open door sound
        //FMODUnity.RuntimeManager.PlayOneShot("event:/Puerta 2/openBigDoor");
    }

    public override void Deactivate()
    {

        



    }
    
}
