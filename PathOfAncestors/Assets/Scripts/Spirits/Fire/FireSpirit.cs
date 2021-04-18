using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpirit : BaseSpirit
{
    [Header ("Only when not interacting")]
    //float to determine the distance to stop when not going to interact
    [SerializeField]
    private float stoppingDistance;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        spiritType = Type.FIRE;
        InitialiseValues();
        //play fire spirit invokation sound
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Invocaciones/invokeFireSpirit", gameObject);
        StartCoroutine(CheckChangeOfState());
    }

    // Update is called once per frame
    void Update()
    {
        //when the spirit is doing the invoking or uninvoking animation, don't move
        if(animController.invoked && !animController.uninvoked)
        {
            //if not one of the following targets, stop so it doesn't go through it
            if((targetTag != "Torch" && targetTag != "OvenActivator" && targetTag != "Burnable" && targetTag != "FireWindInvokation"))
            {
                //if close enough in the x axis, activate collision with gameobject
                if (Vector3.Distance(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), new Vector3(goToPosition.x, gameObject.transform.position.y, gameObject.transform.position.z)) < stoppingDistance)
                {
                    rb.isKinematic = false;
                }
                ////if close enough in the y axis, activate collision with gameobject
                //else if(Vector3.Distance(new Vector3(gameObject.transform.position.x,gameObject.transform.position.y, gameObject.transform.position.z), new Vector3(gameObject.transform.position.x, goToPosition.y,gameObject.transform.position.z)) < stoppingDistance)
                //{
                //    rb.isKinematic = false;
                //}
                //if close enough in the z axis, activate collision with gameobject
                else if (Vector3.Distance(new Vector3(goToPosition.x, gameObject.transform.position.y, goToPosition.z), new Vector3(gameObject.transform.position.x, goToPosition.y, gameObject.transform.position.z)) < stoppingDistance)
                {
                    rb.isKinematic = false;
                }

                //if (Vector3.Distance(gameObject.transform.position, goToPosition) < stoppingDistance)
                //{
                //    rb.isKinematic = false;
                //}
                //else
                //{
                //    FollowOrder();
                //    rb.isKinematic = true;
                //}
                FollowOrder();
            }
            //any other target, act normally
            else
            {
                rb.isKinematic = true;
                FollowOrder();
            }
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

    //check if there has been a change of state, used to avoid checking every frame
    private IEnumerator CheckChangeOfState()
    {
        while (true)
        {
            if (state == States.FOLLOWING)
            {
                targetTag = "FireWindInvokation";
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
