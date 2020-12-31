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
    Vector3 destination;
    public float walkSpeed, runSpeed;
    public float stopDistance = 2f;
    private bool waiting; //bool that will allow the spirit to leave the waiting state
    Vector3 goToPosition;



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
                navAgent.speed = walkSpeed;
                if (Vector3.Distance(destination, target.transform.position) > 1.0f)
                {
                    destination = target.transform.position;
                    navAgent.destination = destination;
                }
                   
                break;

            case States.GOING:
                navAgent.speed = runSpeed;
                navAgent.SetDestination(goToPosition);
                break;
        }
    }

    protected virtual void InitialiseValues()
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        destination = navAgent.destination;
        state = States.FOLLOWING;
        navAgent.speed = walkSpeed;
    
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

}
