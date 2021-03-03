﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseSpirit : MonoBehaviour
{
    public enum Type
    {
        FIRE,
        EARTH,
        WIND
    }

    public enum States { FOLLOWING, GOING, WAITING }; //Follow the player/Go to where the player has said

    protected Type spiritType;
    public States state;

    public NavMeshAgent navAgent;
    protected GameObject player;
    public GameObject target;
    public float walkSpeed, runSpeed;
    public float stopDistance = 2f;
    private bool waiting; //bool that will allow the spirit to leave the waiting state
    Vector3 goToPosition;

    //fire/wind spirit variables
    public float followSpeed;
    public float slowdownDistance;

    protected Vector3 velocity;
    protected Vector3 targetDistance;
    protected Vector3 desiredVelocity;
    protected Vector3 steering;
    protected float slowdownFactor;

    //obstacle avoidance variables
    public int numberOfRays = 17;
    public float angle = 90;
    public float rayRange = 2;
    private RaycastHit avoidanceHit;
    private Ray avoidanceRay;
    public float castRayTime = 0.5f;
    private float castTime = 0;
    public float avoidanceMult = 2f;
    public LayerMask ignoreMask;

    //bool to determine when to change from nav mesh agent to steering behavior and viceversa
    protected bool switchToSteering;
    protected bool edgeOfFloor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void FollowOrder()
    {
        switch (state)
        {
            case States.FOLLOWING:
                if(spiritType == Type.EARTH)
                {
                    if (switchToSteering)
                    {
                        navAgent.enabled = false;
                        if (edgeOfFloor)
                        {
                            followSpeed = 0;
                        }
                        else
                        {
                            followSpeed = runSpeed;
                        }
                        SteeringBehaviorEarth(target.transform.position);
                    }
                    else
                    {
                        navAgent.enabled = true;
                        navAgent.speed = walkSpeed;
                        navAgent.SetDestination(target.transform.position);
                    }
                }
                else
                {
                    SteeringBehavior(target.transform.position);
                }
                break;

            case States.GOING:
                if (spiritType == Type.EARTH)
                {
                    if (switchToSteering)
                    {
                        navAgent.enabled = false;
                        if (edgeOfFloor)
                        {
                            followSpeed = 0;
                        }
                        else
                        {
                            followSpeed = runSpeed;
                        }
                        SteeringBehaviorEarth(goToPosition);
                    }
                    else
                    {
                        navAgent.enabled = true;
                        navAgent.speed = runSpeed;
                        navAgent.SetDestination(goToPosition);
                    }
                }
                else
                {
                    SteeringBehavior(goToPosition);
                }
                break;
        }
    }

    protected virtual void InitialiseValues()
    {
        if (spiritType == Type.EARTH)
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            navAgent.speed = walkSpeed;
            switchToSteering = false;
        }
        state = States.FOLLOWING;
    }

    public void MoveTo( Vector3 targetPos)
    {
        state = States.GOING;
        goToPosition = targetPos;
    }

    public Type GetSpiritType()
    {
        return spiritType;
    }

    public void ReturnToPlayer()
    {
        state = States.FOLLOWING;
    }

    //movement for the fire and wind spirit
    protected void SteeringBehavior(Vector3 _targetPosition)
    {
        //direction in which the character has to move
        targetDistance = (_targetPosition - gameObject.transform.position);

        //change the desired direction to avoid the obstacle
        if (castTime < castRayTime)
        {
            castTime += Time.deltaTime;
        }
        else
        {
            castTime = 0;
            //create the rays for obstacle avoidance
            for (int i = 0; i < numberOfRays; i++)
            {
                //get current rotation
                Quaternion rotation = gameObject.transform.rotation;
                //rotate the ray around an axis
                Quaternion rotationMod = Quaternion.AngleAxis((i / ((float)numberOfRays - 1)) * angle * 2 - angle, gameObject.transform.up);
                //calculate the direction each ray is pointing
                Vector3 direction = rotation * rotationMod * Vector3.forward;

                avoidanceRay = new Ray(gameObject.transform.position, direction);
                if (Physics.Raycast(avoidanceRay, out avoidanceHit, rayRange, ~ignoreMask))
                {
                    targetDistance -= (1.0f / numberOfRays) * direction * avoidanceMult;
                }
            }
        }

        //the desired velocity the character needs in order to go to he target
        desiredVelocity = targetDistance.normalized * followSpeed;
        //the force needed in order to move to the target
        steering = desiredVelocity - velocity;
        //update current velocity
        velocity += steering;
        //Calculate slowdown factor
        slowdownFactor = Mathf.Clamp01(targetDistance.magnitude / slowdownDistance);
        velocity *= slowdownFactor;
        //update current position
        gameObject.transform.position += velocity * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.LookRotation(targetDistance, Vector3.up);
    }

    //movement for the earth spirit on moving platforms
    protected void SteeringBehaviorEarth(Vector3 _targetPosition)
    {
        //direction in which the character has to move
        targetDistance = (new Vector3(_targetPosition.x, gameObject.transform.position.y, _targetPosition.z) - gameObject.transform.position);
        //the desired velocity the character needs in order to go to the target
        desiredVelocity = targetDistance.normalized * followSpeed;
        //the force needed in order to move to the target
        steering = desiredVelocity - velocity;
        //update current velocity
        velocity += steering;
        //Calculate slowdown factor
        slowdownFactor = Mathf.Clamp01(targetDistance.magnitude / slowdownDistance);
        velocity *= slowdownFactor;
        //update current position
        gameObject.transform.position += velocity * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.LookRotation(targetDistance, Vector3.up);
    }

    private void OnDrawGizmos()
    {
        if (castTime >= castRayTime)
        {
            for (int i = 0; i < numberOfRays; i++)
            {
                //get current rotation
                Quaternion rotation = gameObject.transform.rotation;
                //rotate the ray around an axis
                Quaternion rotationMod = Quaternion.AngleAxis((i / ((float)numberOfRays - 1)) * angle * 2 - angle, gameObject.transform.up);
                //calculate the direction each ray is pointing
                Vector3 direction = rotation * rotationMod * Vector3.forward;
                Gizmos.DrawRay(gameObject.transform.position, direction * rayRange);
            }
        }
    }
}
