using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireSpirit : BaseSpirit
{
    private RaycastHit hit;
    private RaycastHit hit2;
    public GameObject pathFollower;
    // Start is called before the first frame update
    void Start()
    {
        spiritType = Type.FIRE;
        InitialiseValues();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(switchToSteering);
        FollowOrder();
        //make the path follower stay at floor y posiiton
        if(Physics.Raycast(fireSpirit.transform.position, Vector3.down, out hit))
        {
            pathFollower.transform.position = new Vector3(pathFollower.transform.position.x, hit.point.y, pathFollower.transform.position.z);
        }
        switchToSteering = DetectObstacleRay();
        
        if(state == States.FOLLOWING)
        {
            Debug.DrawRay(fireSpirit.transform.position, target.transform.position - fireSpirit.transform.position);
        }
        else if (state == States.GOING)
        {
            Debug.DrawRay(fireSpirit.transform.position, fireSpiritHit.point - fireSpirit.transform.position);
        }

        if (!switchToSteering)
        {
            if(state == States.GOING)
            {
                fireSpirit.transform.position = new Vector3(pathFollower.transform.position.x, hit.collider.gameObject.transform.position.y + 0.5f, pathFollower.transform.position.z);
                fireSpirit.transform.rotation = pathFollower.transform.rotation;
            }
            else
            {
                fireSpirit.transform.position = new Vector3(pathFollower.transform.position.x, target.transform.position.y, pathFollower.transform.position.z);
                fireSpirit.transform.rotation = pathFollower.transform.rotation;
            }
        }
        else
        {
            pathFollower.transform.position = new Vector3(fireSpirit.transform.position.x, hit.point.y, fireSpirit.transform.position.z);
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
        if(state == States.FOLLOWING)
        {
            if (Physics.Raycast(fireSpirit.transform.position, target.transform.position - fireSpirit.transform.position, out hit2))
            {
                if (hit2.collider.gameObject.CompareTag("Player"))
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
            if (Physics.Raycast(fireSpirit.transform.position, fireSpiritHit.point - fireSpirit.transform.position, out hit2))
            {
                Debug.Log("hit: " + hit2.collider.gameObject.name);
                Debug.Log("target: " + fireSpiritHit.collider.gameObject.name);
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
}
