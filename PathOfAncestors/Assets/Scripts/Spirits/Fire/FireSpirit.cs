using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpirit : BaseSpirit
{
    
    // Start is called before the first frame update
    void Start()
    {
        DataManager.totalTimesInvokedFire++;
        spiritType = Type.FIRE;
        InitialiseValues();
        //play fire spirit invokation sound
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Invocaciones/invokeFireSpirit", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //when the spirit is doing the invoking or uninvoking animation, don't move
        if(animController.invoked && !animController.uninvoked)
        {
            FollowOrder();
        }
    }

    protected override void InitialiseValues()
    {
        target = GameObject.Find("FireWindInvokation");
        lookAtObjectFire = GameObject.FindGameObjectWithTag("FireLookAt");
        animController = gameObject.GetComponentInChildren<SpiritsAnimatorController>();
        //face the look at object when spawning
        gameObject.transform.rotation = Quaternion.LookRotation(lookAtObjectFire.transform.position - gameObject.transform.position, Vector3.up);
    }
}
