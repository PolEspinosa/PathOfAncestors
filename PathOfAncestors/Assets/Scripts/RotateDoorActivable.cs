using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateDoorActivable : Activable
{

    public GameObject rightDoor;
    public GameObject leftDoor;

    private FMOD.Studio.EventInstance doorSoundInstance;

    // Start is called before the first frame update
    void Start()
    {
        doorSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Puerta 2/openBigDoor");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(doorSoundInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
        AssociateActions();
    }
    
    public override void Activate()
    {
        rightDoor.transform.DORotateQuaternion(Quaternion.Euler(0, 100, 0), 3f);
        leftDoor.transform.DORotateQuaternion(Quaternion.Euler(0, -100,0), 3f);
        //play open door sound
        doorSoundInstance.start();
        StartCoroutine(StopSound());
    }

    public override void Deactivate()
    {

        



    }
    
    private IEnumerator StopSound()
    {
        yield return new WaitForSeconds(3f);
        doorSoundInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
