using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritTut : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> waypoints;

    private Vector3 targetDistance, desiredVelocity, velocity, steering;
    [SerializeField]
    private float movingSpeed, slowdownDistance;
    [SerializeField]
    private SpiritsAnimatorController animController;
    private float slowdownFactor;
    private bool move;
    private int waypointIndex;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        move = false;
        waypointIndex = 0;
        player = GameObject.Find("Character");
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
            CurrentLookAt(player.transform.position + new Vector3(0, 1f, 0));
        }
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

        CurrentLookAt(_lookAt);

        //set the speed to the animator variable for the blend tree
        animController.speed = velocity.magnitude / movingSpeed;
    }

    private void CurrentLookAt(Vector3 _lookAt)
    {
        gameObject.transform.rotation = Quaternion.LookRotation(_lookAt, Vector3.up);
    }
}
