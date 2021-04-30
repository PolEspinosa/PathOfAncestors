using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EarthSpirit : BaseSpirit
{
    public GameObject rayStart;
    private float angle, rotationTime;
    public float rotationSpeed;
    private Quaternion targetRotation;
    private bool hasToRotate;
    private float runTmpSpeed, walkTmpSpeed;
    [SerializeField]
    private AnimationCurve rotationCurve;
    [SerializeField]
    private float interactionDistance;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseValues();
        spiritType = Type.EARTH;
        edgeOfFloor = false;
        //play earth spirit invokation sound
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Invocaciones/invokeEarthSpirit", gameObject);
        hasToRotate = false;
        runTmpSpeed = runSpeed;
        walkTmpSpeed = walkSpeed;
        navMeshPath = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            if (targetObject.CompareTag("BreakableWall") && Vector3.Distance(gameObject.transform.position, targetObject.transform.position) < interactionDistance)
            {
                animController.hasToBreak = true;
            }
        }
        else
        {
            animController.hasToBreak = false;
        }
        //ApplyNewRotation();
        if (animController.invoked && !animController.uninvoked)
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
        }
    }

    protected override void InitialiseValues()
    {
        order = GameObject.Find("Character").GetComponent<OrderSystem>();
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
            case "BreakWallTrigger":
                targetObject = null;
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

    private void ApplyNewRotation()
    {
        if (state == States.GOING)
        {
            Vector3 directionForward = new Vector3(goToPosition.x - gameObject.transform.position.x, 0, goToPosition.z - gameObject.transform.position.z);
            angle = Vector3.Angle(gameObject.transform.forward.normalized, directionForward.normalized);
            angle = Mathf.Abs(angle);
            runSpeed = rotationCurve.Evaluate(angle / 180f) * runTmpSpeed;
            if (angle > 40f && !hasToRotate)
            {
                hasToRotate = true;
            }
            if (hasToRotate)
            {
                if (angle >= 20)
                {
                    runSpeed = rotationCurve.Evaluate(angle / 180f) * runTmpSpeed;
                }
                else
                {
                    runSpeed = runTmpSpeed;
                    hasToRotate = false;
                }
            }
        }
        else if(state == States.FOLLOWING)
        {
            Vector3 directionForward = new Vector3(target.transform.position.x - gameObject.transform.position.x, 0, target.transform.position.z - gameObject.transform.position.z);
            angle = Vector3.Angle(gameObject.transform.forward.normalized, directionForward.normalized);
            angle = Mathf.Abs(angle);
            walkSpeed = rotationCurve.Evaluate(angle / 180f) * runTmpSpeed;
            if (angle > 40f && !hasToRotate)
            {
                hasToRotate = true;
            }
            if (hasToRotate)
            {
                if (angle >= 20)
                {
                   walkSpeed = rotationCurve.Evaluate(angle / 180f) * runTmpSpeed;
                }
                else
                {
                    walkSpeed = walkTmpSpeed;
                    hasToRotate = false;
                }
            }
        }
    }

    //public bool HasPath()
    //{
    //    navAgent.CalculatePath(goToPosition, navMeshPath);
    //    if (navMeshPath.status != NavMeshPathStatus.PathComplete)
    //    {
    //        return false;
    //    }
    //    return true;
    //}
}
