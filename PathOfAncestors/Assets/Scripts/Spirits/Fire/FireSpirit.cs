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
        //when the spirit is doing the invoking or uninvoking animation, don't move
        if(animController.invoked && !animController.uninvoked)
        {
            FollowOrder();
        }
    }

    protected override void InitialiseValues()
    {
        target = GameObject.Find("FireWindInvokation");
        player = GameObject.FindGameObjectWithTag("Player");
        animController = gameObject.GetComponentInChildren<SpiritsAnimatorController>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("col");
    }
}
