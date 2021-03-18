using System.Collections;
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
    //only for fire spirit
    public RaycastHit fireSpiritHit;
    //raycast used to know which target was the previous one, only for fire spirit
    public RaycastHit newFireSpiritTarget;
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
                else if(spiritType == Type.FIRE)
                {
                    if (switchToSteering)
                    {
                        SteeringBehavior(target.transform.position);
                        navAgent.enabled = false;
                    }
                    else
                    {
                        navAgent.enabled = true;
                        navAgent.speed = walkSpeed;
                        navAgent.SetDestination(target.transform.position);
                        //vertical movement for the fire spirit when using nav mesh
                        SteeringBehaviorFireY(target.transform.position);
                    }
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
                else if(spiritType == Type.FIRE)
                {
                    if (switchToSteering)
                    {
                        navAgent.enabled = false;
                        SteeringBehavior(goToPosition);
                    }
                    else
                    {
                        navAgent.enabled = true;
                        navAgent.speed = runSpeed;
                        navAgent.SetDestination(goToPosition);
                        //vertical movement for the fire spirit when using nav mesh
                        SteeringBehaviorFireY(goToPosition);
                    }
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
        else if (spiritType == Type.FIRE)
        {
            switchToSteering = true;
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

    //movement for the fire spirit
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
        gameObject.transform.rotation = Quaternion.LookRotation(targetDistance, Vector3.up);
    }

    //steering behavior for the fire spirit in the Y axiswhen it is using the navmesh
    protected void SteeringBehaviorFireY(Vector3 _targetPosition)
    {
        //direction in which the character has to move
        targetDistance = _targetPosition - gameObject.transform.position;
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
        gameObject.transform.position += new Vector3(0, velocity.y, 0) * Time.deltaTime;
        //fireSpirit.transform.rotation = Quaternion.LookRotation(targetDistance, Vector3.up);
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
}
