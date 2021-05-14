using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritTut : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> waypoints;

    private Vector3 targetDistance, desiredVelocity, velocity, steering;
    [SerializeField]
    private float movingSpeed, slowdownDistance, fleeDistance, currentSpeed;
    [SerializeField]
    private SpiritsAnimatorController animController;
    private float slowdownFactor;
    private bool move, updateIndex, canUpdateIndex;
    private int waypointIndex;
    private GameObject player, target;
    private GameObject lookAt;
    private float currentTime;
    bool canMove = false;
    public GameObject onboarding;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMove(1.5f));
        move = false;
        waypointIndex = 0;
        player = GameObject.Find("Character");
        updateIndex = false;
        lookAt = GameObject.FindGameObjectWithTag("FireLookAt");
        canUpdateIndex = false;
        targetDistance = waypoints[waypointIndex].transform.position - gameObject.transform.position;
        target = GameObject.Find("PickUp");
        //play fire spirit invokation sound
        FMOD.Studio.EventInstance fireSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Invocaciones/invokeFireSpirit");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fireSoundInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
        fireSoundInstance.setVolume(4f);
        fireSoundInstance.start();
        currentSpeed = movingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();
        UpdateWaypointsIndex();
        //if the spirit has to move, move to next waypoint
        if (canMove && move && !NoInputFire.noInput)
        {
            MoveTo(waypoints[waypointIndex].transform.position, targetDistance);
            currentSpeed = movingSpeed + (10f / (Vector3.Distance(gameObject.transform.position, target.transform.position)));
        }
        else
        {
            CurrentLookAt(target.transform);
        }
        Uninvoke();
        DestroySpirit();
        if (NoInputFire.noInput)
        {
            if(currentTime < 2f)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                MoveTo(player.transform.position + new Vector3(0, 3f, 0), targetDistance);
                slowdownDistance = 7f;
                currentSpeed = 5;
            }
        }
    }

    private void MoveTo(Vector3 _targetPosition, Vector3 _lookAt)
    {
        //direction in which the character has to move
        targetDistance = (_targetPosition - gameObject.transform.position);
        //the desired velocity the character needs in order to go to he target
        desiredVelocity = targetDistance.normalized * currentSpeed;
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
        animController.speed = velocity.magnitude / currentSpeed;
    }

    private void CurrentLookAt(Transform _lookAt)
    {
        transform.LookAt(_lookAt, Vector3.up);
    }

    private void UpdateMove()
    {
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) < fleeDistance)
        {
            if (waypointIndex < waypoints.Count)
            {
                move = true;
                updateIndex = true;
            }
        }
        if (Vector3.Distance(waypoints[waypointIndex].transform.position, gameObject.transform.position) < 0.1f)
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

    private void Uninvoke()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position + new Vector3(0, 3f, 0)) < 1.5f && NoInputFire.noInput)
        {
            animController.uninvoked = true;
            currentSpeed = 0;
        }
    }

    private void DestroySpirit()
    {
        if (animController.destroySpirit)
        {
            onboarding.GetComponent<Onboarding>().ActiveFireTut();
            Destroy(gameObject);
        }
    }

    IEnumerator  StartMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canMove = true;
    }
}
