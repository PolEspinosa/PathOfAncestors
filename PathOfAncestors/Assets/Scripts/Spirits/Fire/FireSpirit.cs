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
        
        //see if there are any obstacles in front of the fire spirit
        if (Physics.Raycast(fireSpirit.transform.position, fireSpirit.transform.forward, out hit2, 5))
        {
            Debug.Log(hit2.collider.gameObject.tag);
            if (state == States.FOLLOWING)
            {
                if (hit2.collider.gameObject.tag == "Player")
                {
                    switchToSteering = true;
                }
                else
                {
                    switchToSteering = false;
                }
            }
        }
        else
        {
            switchToSteering = true;
        }
        ////see if there are any obstacles at the right of the fire spirit
        //else if (Physics.Raycast(fireSpirit.transform.position, fireSpirit.transform.forward + fireSpirit.transform.right / 2, out hit2, 5))
        //{
        //    if ((hit2.collider.gameObject.tag == fireSpiritHit.collider.gameObject.tag) || hit2.collider.gameObject == null || fireSpiritHit.collider.gameObject == null)
        //    {
        //        switchToSteering = true;
        //    }
        //    else
        //    {
        //        switchToSteering = false;
        //    }
        //}
        ////see if there are any obstacles at the left of the fire spirit
        //else if (Physics.Raycast(fireSpirit.transform.position, fireSpirit.transform.forward - fireSpirit.transform.right / 2, out hit2, 5))
        //{
        //    if ((hit2.collider.gameObject.tag == fireSpiritHit.collider.gameObject.tag) || hit2.collider.gameObject == null || fireSpiritHit.collider.gameObject == null)
        //    {
        //        switchToSteering = true;
        //    }
        //    else
        //    {
        //        switchToSteering = false;
        //    }
        //}
        //right ray
        Debug.DrawRay(fireSpirit.transform.position, (fireSpirit.transform.forward + fireSpirit.transform.right / 2) * 5);
        //front ray
        Debug.DrawRay(fireSpirit.transform.position, (fireSpirit.transform.forward) * 5);
        //left ray
        Debug.DrawRay(fireSpirit.transform.position, (fireSpirit.transform.forward - fireSpirit.transform.right / 2) * 5);
        if (!switchToSteering)
        {
            if(state == States.GOING)
            {
                fireSpirit.transform.position = new Vector3(pathFollower.transform.position.x, hit.collider.gameObject.transform.position.y + 0.5f, pathFollower.transform.position.z);
                fireSpirit.transform.rotation = pathFollower.transform.rotation;
            }
            else
            {
                fireSpirit.transform.position = new Vector3(pathFollower.transform.position.x, target.transform.position.y + 0.5f, pathFollower.transform.position.z);
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

    
}
