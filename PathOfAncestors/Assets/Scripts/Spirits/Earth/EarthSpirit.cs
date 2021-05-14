using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EarthSpirit : BaseSpirit
{
    public GameObject rayStart;
    private float angle, rotationTime;
    public float extraRotationSpeed;
    private Quaternion targetRotation;
    private bool hasToRotate;
    private float runTmpSpeed, walkTmpSpeed;
    [SerializeField]
    private AnimationCurve rotationCurve;
    [SerializeField]
    private float interactionDistance;

    [SerializeField]
    private Outline outlineScript;
    [SerializeField]
    private LayerMask ignoreMask;

    private FMOD.Studio.EventInstance stepsInstance;
    [SerializeField]
    private float walkStepDelay, runStepDelay;
    private float currentWalkStepDelayTime, currentRunStepDelayTime;
    private GameObject parentObject;
    private float delay;
    public bool onPressurePlate, onPlatform;
    private float sitToIdleDelay;

    // Start is called before the first frame update
    void Start()
    {
        DataManager.totalTimesInvokedEarth++;
        InitialiseValues();
        spiritType = Type.EARTH;
        edgeOfFloor = false;
        //play earth spirit invokation sound
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Invocaciones/invokeEarthSpirit", gameObject);
        hasToRotate = false;
        runTmpSpeed = runSpeed;
        walkTmpSpeed = walkSpeed;
        navMeshPath = new NavMeshPath();
        //col = gameObject.GetComponent<CapsuleCollider>();
        player = GameObject.Find("Character");
        //delay for the earth spirit step sounds
        currentWalkStepDelayTime = walkStepDelay;
        currentRunStepDelayTime = runStepDelay;
        onPressurePlate = false;
        onPlatform = false;
        sitToIdleDelay = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        ControlSitAnimation();
        if (targetObject != null)
        {
            if (!targetObject.CompareTag("MovingPlatform"))
            {
                onPlatform = false;
            }
            else if(targetObject.CompareTag("MovingPlatform") && targetObject != parentObject)
            {
                onPlatform = false;
            }
            if (!targetObject.CompareTag("PressurePlateActivator"))
            {
                onPressurePlate = false;
            }
            else if (targetObject.CompareTag("PressurePlateActivator") && navAgent.remainingDistance > 1f)
            {
                onPressurePlate = false;
            }
            if (targetObject.CompareTag("BreakableWall") && Vector3.Distance(gameObject.transform.position, targetObject.transform.position) < interactionDistance)
            {
                animController.hasToBreak = true;
            }
            else
            {
                animController.hasToBreak = false;
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
            if (parentObject != null)
            {
                gameObject.transform.parent=parentObject.transform;
            }
            //outlineScript.enabled = true;
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
            //ExtraRotationSteering();
        }
        else
        {
            if (animController.moveAfterGetUp)
            {
                ExtraRotation();
            }
        }
        //if the spirit is being seen, disable the outline
        //if (IsSeen())
        //{
        //    outlineScript.enabled = false;
        //}
        //else
        //{
        //    outlineScript.enabled = true;
        //}
        //if (navAgent.velocity.magnitude > 0.1f)
        //{
        //    //PlayStepSound();
        //}
        //else
        //{
        //    currentWalkStepDelayTime = walkStepDelay;
        //    currentRunStepDelayTime = runStepDelay;
        //}
        //ControlGetUpDelay();
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
                parentObject = other.gameObject;
                onPlatform = true;
                //gameObject.transform.parent = other.gameObject.transform;
                break;
            case "BreakWallTrigger":
                targetObject = null;
                break;
            case "EarthActivator":
                outlineScript.enabled = false;
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
                parentObject = null;
                onPlatform = false;
                break;
            case "EarthActivator":
                outlineScript.enabled = true;
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

    private void ExtraRotation()
    {
        if (navAgent.remainingDistance > 1)
        {
            Vector3 lookRotation = navAgent.steeringTarget - gameObject.transform.position;
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), extraRotationSpeed * Time.deltaTime);
        }
    }

    private void ExtraRotationSteering()
    {
        if (targetDistance.magnitude > 0.1f)
        {
            Vector3 lookRotation = targetDistance - gameObject.transform.position;
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), extraRotationSpeed * Time.deltaTime);
        }
    }

    private bool IsSeen()
    {
        Debug.DrawRay(gameObject.transform.position, player.transform.position - (new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1f, gameObject.transform.position.z)), Color.red);
        RaycastHit hit;
        if(Physics.Raycast(gameObject.transform.position, player.transform.position - gameObject.transform.position, out hit, Mathf.Infinity, ~ignoreMask))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void PlayStepSound()
    {
        if (state == States.FOLLOWING)
        {
            if (currentWalkStepDelayTime > 0)
            {
                currentWalkStepDelayTime -= Time.deltaTime;
            }
            else
            {
                stepsInstance = FMODUnity.RuntimeManager.CreateInstance("event:/EarthSteps/earthSpiritSteps");
                stepsInstance.setVolume(0.5f);
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(stepsInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
                stepsInstance.start();
                stepsInstance.release();
                currentWalkStepDelayTime = walkStepDelay;
            }
        }
        else if(state == States.GOING)
        {
            if (currentRunStepDelayTime > 0)
            {
                currentRunStepDelayTime -= Time.deltaTime;
            }
            else
            {
                stepsInstance = FMODUnity.RuntimeManager.CreateInstance("event:/EarthSteps/earthSpiritSteps");
                stepsInstance.setVolume(0.5f);
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(stepsInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
                stepsInstance.start();
                stepsInstance.release();
                currentRunStepDelayTime = runStepDelay;
            }
        }
    }

    private void ControlSitAnimation()
    {
        if (state == States.FOLLOWING)
        {
            animController.stateString = "FOLLOWING";
            animController.going = false;
            delay = 0;
        }
        else if (state == States.GOING)
        {
            if (delay < 0.1f)
            {
                delay += Time.deltaTime;
            }
            else
            {
                if (!switchToSteering)
                {
                    if (navAgent.remainingDistance < 1f || onPressurePlate)
                    {
                        animController.going = true;
                    }
                    else
                    {
                        animController.going = false;
                    }
                }
                else
                {
                    if (Vector3.Distance(goToPosition, gameObject.transform.position) < 1f || onPlatform)
                    {
                        animController.going = true;
                    }
                    else
                    {
                        animController.going = false;
                    }
                }
            }
            animController.stateString = "GOING";

        }
    }

    private void ControlGetUpDelay()
    {
        if (!animController.going)
        {
            if (sitToIdleDelay < 0.2f)
            {
                sitToIdleDelay += Time.deltaTime;
            }
        }
        else
        {
            sitToIdleDelay = 0;
        }
    }

    
}
