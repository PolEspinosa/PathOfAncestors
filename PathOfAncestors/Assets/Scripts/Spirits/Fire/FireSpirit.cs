using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpirit : BaseSpirit
{
    
    // Start is called before the first frame update
    void Start()
    {
        spiritType = Type.FIRE;
        InitialiseValues();
        //play fire spirit invokation sound
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Invocaciones/invokeFireSpirit", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        FollowOrder();
    }

    protected override void InitialiseValues()
    {
        target = GameObject.Find("FireWindInvokation");
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("col");
    }
}
