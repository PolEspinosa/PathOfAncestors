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
                    navAgent.speed = walkSpeed;
                    navAgent.SetDestination(target.transform.position);
                }
                else
                {
                    SteeringBehavior(target.transform.position);
                }
                break;

            case States.GOING:
                if (spiritType == Type.EARTH)
                {
                    navAgent.speed = runSpeed;
                    navAgent.SetDestination(goToPosition);
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
    }
}