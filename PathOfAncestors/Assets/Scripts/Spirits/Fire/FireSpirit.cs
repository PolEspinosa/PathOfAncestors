using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpirit : BaseSpirit
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        spiritType = Type.FIRE;
        InitialiseValues();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowOrder();
        //if(Vector3.Distance(gameObject.transform.position,target.transform.position) < 2.0f)
        //{
        //    rb.isKinematic = true;
        //}
        //else
        //{
        //    rb.isKinematic = false;
        //}
    }

    protected override void InitialiseValues()
    {
        target = GameObject.Find("FireWindInvokation");
    }
}
