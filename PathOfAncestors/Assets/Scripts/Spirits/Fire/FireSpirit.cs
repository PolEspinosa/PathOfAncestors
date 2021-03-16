using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireSpirit : BaseSpirit
{
    private RaycastHit hit;
    private RaycastHit hit2;
    private GameObject pathFollower;
    private bool castRay;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        pathFollower = GameObject.FindGameObjectWithTag("PathFollower");
        spiritType = Type.FIRE;
        InitialiseValues();
        castRay = true;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowOrder();
        //make the path follower stay at floor y posiiton
        if(Physics.Raycast(gameObject.transform.position, Vector3.down, out hit))
        {
            pathFollower.transform.position = new Vector3(pathFollower.transform.position.x, hit.point.y, pathFollower.transform.position.z);
        }
        //variable that determines whether to use steering behavior or nav mesh agent
        switchToSteering = DetectObstacleRay();

        //if casting a ray, debug of the rays
        if (castRay)
        {
            if (state == States.FOLLOWING)
            {
                Debug.DrawRay(gameObject.transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z) - gameObject.transform.position);
            }
            else if (state == States.GOING)
            {
                Debug.DrawRay(gameObject.transform.position, fireSpiritHit.point - gameObject.transform.position);
            }
        }
        
        //if the spirit is using the nav mesh agent
        if (!switchToSteering)
        {
            //when going to told position
            if(state == States.GOING)
            {
                //the gameobject follows the path follower (nav mesh agent)
                gameObject.transform.position = new Vector3(pathFollower.transform.position.x, gameObject.transform.position.y + 0.5f, pathFollower.transform.position.z);
                gameObject.transform.rotation = pathFollower.transform.rotation;
            }
            //if following the player
            else
            {
                gameObject.transform.position = new Vector3(pathFollower.transform.position.x, gameObject.transform.position.y, pathFollower.transform.position.z);
                gameObject.transform.rotation = pathFollower.transform.rotation;
            }
        }
        //if the spirit is using the steering behavior
        else
        {
            //set the pathfollower to position of the spirit but on floor y position
            pathFollower.transform.position = new Vector3(gameObject.transform.position.x, hit.point.y, gameObject.transform.position.z);
        }
        //when close enough to the told position, diable ray so it uses steering behavior to avoid problems
        if(Vector3.Distance(gameObject.transform.position,fireSpiritHit.point) < 5)
        {
            if((fireSpiritHit.collider.gameObject.CompareTag("OvenActivator") || fireSpiritHit.collider.gameObject.CompareTag("Torch")))
            {
                castRay = false;
            }
        }
        else
        {
            castRay = true;
        }

        if (state == States.FOLLOWING && Vector3.Distance(gameObject.transform.position,player.transform.position) < 5)
        {
            rb.isKinematic = true;
        }
        //if the pointed gameobject is one of the following, activate isKinematic so it can go through the colliders
        else if(fireSpiritHit.collider.gameObject != null && (fireSpiritHit.collider.gameObject.CompareTag("OvenActivator") || fireSpiritHit.collider.gameObject.CompareTag("Torch")))
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
    }

    protected override void InitialiseValues()
    {
        target = GameObject.Find("FireWindInvokation");
        navAgent = pathFollower.GetComponent<NavMeshAgent>();
        switchToSteering = true;
    }

    private bool DetectObstacleRay()
    {
        if (castRay)
        {
            if (state == States.FOLLOWING)
            {
                if (Physics.Raycast(gameObject.transform.position, new Vector3(player.transform.position.x,player.transform.position.y + 1, player.transform.position.z) - gameObject.transform.position, out hit2))
                {
                    //Debug.Log(hit2.collider.gameObject.name);
                    if (hit2.collider.gameObject.name == "FireWindInvokation" || hit2.collider.gameObject.CompareTag("Player"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (Physics.Raycast(gameObject.transform.position, fireSpiritHit.point - gameObject.transform.position, out hit2))
                {
                    if (hit2.collider.gameObject.name == fireSpiritHit.collider.gameObject.name)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
        else
        {
            return true;
        }
    }
}
