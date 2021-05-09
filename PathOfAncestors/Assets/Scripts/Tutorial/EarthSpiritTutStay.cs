using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EarthSpiritTutStay : MonoBehaviour
{
    [SerializeField]
    private float extraRotationSpeed, lookAtDistance;
    private GameObject player;
    [SerializeField]
    private SpiritsAnimatorController animController;
    private NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Character");
        navAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ExtraRotation();
        animController.speed = navAgent.velocity.magnitude;
    }

    private void ExtraRotation()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < lookAtDistance)
        {
            Vector3 lookRotation = player.transform.position - gameObject.transform.position;
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), extraRotationSpeed * Time.deltaTime);
            animController.stateString = "GOING";
        }
    }
}
