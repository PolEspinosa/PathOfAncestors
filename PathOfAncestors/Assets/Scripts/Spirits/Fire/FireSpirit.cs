using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireSpirit : BaseSpirit
{
    private RaycastHit hit;
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
        FollowOrder();
        if(Physics.Raycast(gameObject.transform.position, Vector3.down, out hit))
        {
            pathFollower.transform.position = new Vector3(pathFollower.transform.position.x, hit.point.y, pathFollower.transform.position.z);
        }
        Debug.DrawRay(gameObject.transform.position, Vector3.down);
        if (!switchToSteering)
        {
            gameObject.transform.position = new Vector3(pathFollower.transform.position.x, target.transform.position.y, pathFollower.transform.position.z);
            gameObject.transform.rotation = pathFollower.transform.rotation;
        }
    }

    protected override void InitialiseValues()
    {
        target = GameObject.Find("FireWindInvokation");
        navAgent = pathFollower.GetComponent<NavMeshAgent>();
    }

    
}
