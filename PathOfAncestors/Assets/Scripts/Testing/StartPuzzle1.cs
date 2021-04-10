using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPuzzle1 : MonoBehaviour
{
    private OrderSystem orderSystem;
    private SpiritManager spiritManager;
    // Start is called before the first frame update
    void Start()
    {
        orderSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<OrderSystem>();
        spiritManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SpiritManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            orderSystem.timesOrdered = 0;
            spiritManager.timesInvoked = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
