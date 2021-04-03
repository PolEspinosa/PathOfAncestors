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
    //bool to determine when to change from nav mesh agent to steering behavior and viceversa
    protected bool switchToSteering;
    protected bool edgeOfFloor;

    //fire animator variables
    protected SpiritsAnimatorController animController;
    
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
                            followSpeed = walkSpeed;
                        }
                        SteeringBehaviorEarth(target.transform.position);
                    }
                    else
                    {
                        navAgent.enabled = true;
                        navAgent.speed = walkSpeed;
                        navAgent.SetDestination(target.transform.position);

                        //we divide by the run speed so we can blend better the animations due to the velocity scaling factor (0-0.7-1)
                        animController.speed = navAgent.velocity.magnitude / runSpeed;
                    }
                }
                else
                {
                    //we make it look to the direction of the player and thus avoid rotation problems when too close
                    //we add +1.5 because the player has his pivot at the feet
                    SteeringBehavior(target.transform.position, player.transform.position + new Vector3(0, 1.5f, 0) - gameObject.transform.position);
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

                        //we divide by the run speed so we can blend better the animations due to the velocity scaling factor (0-0.7-1)
                        animController.speed = navAgent.velocity.magnitude / runSpeed;
                    }
                }
                else
                {
                    SteeringBehavior(goToPosition, targetDistance);
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
    //we add a look at direction to avoid rotation problems when the spirit is too close to the target
    protected void SteeringBehavior(Vector3 _targetPosition, Vector3 _lookAtDirection) 
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
        gameObject.transform.rotation = Quaternion.LookRotation(_lookAtDirection, Vector3.up);


        //set the speed to the animator variable for the blend tree
        animController.speed = velocity.magnitude / followSpeed;
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

        //set the speed to the animator variable for the blend tree
        //we divide by the run speed so we can blend better the animations due to the velocity scaling factor (0-0.7-1)
        animController.speed = velocity.magnitude / runSpeed;
    }
}
