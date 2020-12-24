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

    
}
