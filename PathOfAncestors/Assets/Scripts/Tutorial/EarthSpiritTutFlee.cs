using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EarthSpiritTutFlee : MonoBehaviour
{
    [SerializeField]
    private float fleeDistance, fleeSpeed, extraRotationSpeed;
    private GameObject player;
    [SerializeField]
    private GameObject fleePoint;
    [SerializeField]
    private SpiritsAnimatorController animController;
    private NavMeshAgent navAgent;
    private enum State
    {
        STAY, FLEE
    };
    private State state;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character");
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        state = State.STAY;
        navAgent.speed = fleeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        Uninvoke();
        DestroySpirit();
    }

    private void ExtraRotation()
    {
        if (navAgent.remainingDistance > 1)
        {
            Vector3 lookRotation = navAgent.steeringTarget - gameObject.transform.position;
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), extraRotationSpeed * Time.deltaTime);
        }
    }

    private void UpdateState()
    {
        if(Vector3.Distance(player.transform.position,gameObject.transform.position) < fleeDistance)
        {
            MoveToTarget();
        }
        animController.speed = navAgent.velocity.magnitude / fleeSpeed;
    }

    private void MoveToTarget()
    {
        navAgent.SetDestination(fleePoint.transform.position);
    }

    private void Uninvoke()
    {
        if(Vector3.Distance(gameObject.transform.position, fleePoint.transform.position) < 1f)
        {
            navAgent.speed = 0;
            animController.uninvoked = true;
            navAgent.enabled = false;
        }
    }

    private void DestroySpirit()
    {
        if (animController.destroySpirit)
        {
            Destroy(gameObject);
        }
    }
}
