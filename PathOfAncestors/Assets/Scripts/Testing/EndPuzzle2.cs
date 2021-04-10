using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPuzzle2 : MonoBehaviour
{
    private OrderSystem orderSystem;
    private DataManager dataManager;
    private SpiritManager spiritManager;
    private SpiritsPassiveAbilities spiritsPassive;
    private TimeCounter timeCounter;
    // Start is called before the first frame update
    void Start()
    {
        orderSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<OrderSystem>();
        spiritManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SpiritManager>();
        dataManager = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
        spiritsPassive = GameObject.FindGameObjectWithTag("Player").GetComponent<SpiritsPassiveAbilities>();
        timeCounter = GameObject.FindGameObjectWithTag("Player").GetComponent<TimeCounter>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dataManager.puzzle2TimesOrdered = orderSystem.timesOrdered;
            dataManager.puzzle2TimesInvoked = spiritManager.timesInvoked;
            dataManager.puzzle2TimesMoved = spiritsPassive.timesMoved;
            dataManager.puzzle2TimePassed = timeCounter.timePassed;
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
