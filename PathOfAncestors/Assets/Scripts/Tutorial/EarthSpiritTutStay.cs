using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EarthSpiritTutStay : MonoBehaviour
{
    [SerializeField]
    private float extraRotationSpeed, lookAtDistance, moveSpeed;
    private GameObject player;
    [SerializeField]
    private SpiritsAnimatorController animController;
    private NavMeshAgent navAgent;
    private GameObject target;
    private float currentTime;

    public GameObject onboarding;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character");
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.Find("EarthInvokation");
        navAgent.speed = moveSpeed;
        animController.stateString = "FOLLOWING";
    }

    // Update is called once per frame
    void Update()
    {
        ExtraRotation();
        animController.speed = navAgent.velocity.magnitude / moveSpeed;
        if (NoInputEarth.noInput)
        {
            if(currentTime >= 2f)
            {
                MoveToPlayer();
            }
            else
            {
                currentTime += Time.deltaTime;
            }
            ExtraRotation();
        }
        Uninvoke();
        DestroySpirit();
    }

    private void ExtraRotation()
    {
        //if (Vector3.Distance(gameObject.transform.position, player.transform.position) < lookAtDistance)
        //{
        //    Vector3 lookRotation = player.transform.position - gameObject.transform.position;
        //    gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), extraRotationSpeed * Time.deltaTime);            
        //}
        Vector3 lookRotation = player.transform.position - gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), extraRotationSpeed * Time.deltaTime);
    }

    private void MoveToPlayer()
    {
        navAgent.SetDestination(player.transform.position);
    }

    private void Uninvoke()
    {
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) < 3f)
        {
            animController.uninvoked = true;
            navAgent.speed = 0;
        }
    }

    private void DestroySpirit()
    {
        if (animController.destroySpirit)
        {
            onboarding.GetComponent<Onboarding>().ActiveEartTut();
            Destroy(gameObject);
        }
    }
}
