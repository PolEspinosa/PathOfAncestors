using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritTut : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> waypoints;

    private Vector3 targetDistance, desiredVelocity, velocity, steering;
    [SerializeField]
    private float movingSpeed, slowdownDistance, fleeDistance;
    [SerializeField]
    private SpiritsAnimatorController animController;
    private float slowdownFactor;
    private bool move, updateIndex, canUpdateIndex;
    private int waypointIndex;
    private GameObject player;
    private GameObject lookAt;

    // Start is called before the first frame update
    void Start()
    {
        move = false;
        waypointIndex = 0;
        player = GameObject.Find("Character");
        updateIndex = false;
        lookAt = GameObject.FindGameObjectWithTag("FireLookAt");
        canUpdateIndex = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if the spirit has to move, move to next waypoint
        if (move)
        {
            MoveTo(waypoints[waypointIndex].transform.position, targetDistance);
        }
        else
        {
            CurrentLookAt(lookAt.transform);
        }
        UpdateWaypointsIndex();
        UpdateMove();
    }

    private void MoveTo(Vector3 _targetPosition, Vector3 _lookAt)
    {
        //direction in which the character has to move
        targetDistance = (_targetPosition - gameObject.transform.position);
        //the desired velocity the character needs in order to go to he target
        desiredVelocity = targetDistance.normalized * movingSpeed;
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
        animController.speed = velocity.magnitude / movingSpeed;
    }

    private void CurrentLookAt(Transform _lookAt)
    {
        transform.LookAt(_lookAt, Vector3.up);
    }

    private void UpdateMove()
    {
        if (Vector3.Distance(gameObject.transform.position, lookAt.transform.position) < fleeDistance)
        {
            if (waypointIndex < waypoints.Count - 1)
            {
                move = true;
                updateIndex = true;
            }
        }
        else if (targetDistance.magnitude < 0.1f)
        {
            move = false;
            canUpdateIndex = true;
        }
        
    }

    private void UpdateWaypointsIndex()
    {
        if (updateIndex && canUpdateIndex)
        {
            if (waypointIndex < waypoints.Count - 1)
            {
                waypointIndex++;
                updateIndex = false;
                canUpdateIndex = false;
            }
        }
    }
}
