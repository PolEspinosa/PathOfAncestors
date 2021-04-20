using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpirit : BaseSpirit
{
    public GameObject rayStart;
    // Start is called before the first frame update
    void Start()
    {
        InitialiseValues();
        spiritType = Type.EARTH;
        edgeOfFloor = false;
        //play earth spirit invokation sound
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Invocaciones/invokeEarthSpirit", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(rayStart.transform.position, -rayStart.transform.up , Color.green);
        if (animController.invoked)
        {
            FollowOrder();
        }
        //cast the ray
        if (switchToSteering)
        {
            RaycastHit hit;

            if (Physics.Raycast(rayStart.transform.position, -rayStart.transform.up, out hit, 1))
            {
                edgeOfFloor = false;
            }
            else
            {
                edgeOfFloor = true;
            }
            
            if(targetTag== "MovingPlatform")
            {

            }
        }
    }

    protected override void InitialiseValues()
    {
        target = GameObject.Find("EarthInvokation");
        animController = gameObject.GetComponentInChildren<SpiritsAnimatorController>();
        lookAtObjectEarth = GameObject.FindGameObjectWithTag("EarthLookAt");
        //face the player when spawning
        gameObject.transform.rotation = Quaternion.LookRotation(lookAtObjectEarth.transform.position - gameObject.transform.position, Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PressurePlate":
                other.gameObject.GetComponent<PressurePlate>().active = true;
                break;
            case "SwitchPath":
                switchToSteering = true;
                break;
            case "MovingPlatform":
                gameObject.transform.parent = other.gameObject.transform;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "PressurePlate":
                other.gameObject.GetComponent<PressurePlate>().active = false;
                break;
            case "SwitchPath":
                switchToSteering = false;
                break;
            case "MovingPlatform":
                gameObject.transform.parent = null;
                break;
        }
    }
}
