using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpirit : BaseSpirit
{
 
    // Start is called before the first frame update
    void Start()
    {
        InitialiseValues();
        spiritType = Type.EARTH; 
    }

    // Update is called once per frame
    void Update()
    {
        FollowOrder();
    }

   protected override void InitialiseValues()
    {
        target = GameObject.Find("EarthInvokation");
    }
}
