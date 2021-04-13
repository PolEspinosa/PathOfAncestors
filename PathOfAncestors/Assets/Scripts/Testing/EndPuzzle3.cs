using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPuzzle3 : MonoBehaviour
{
    private OrderSystem orderSystem;
    private DataManager dataManager;
    private SpiritManager spiritManager;
    private SpiritsPassiveAbilities spiritsPassive;
    private TimeCounter timeCounter;
    private PickUpObject pickUp;
    public Void abyss;
    // Start is called before the first frame update
    void Start()
    {
        orderSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<OrderSystem>();
        spiritManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SpiritManager>();
        dataManager = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
        spiritsPassive = GameObject.FindGameObjectWithTag("Player").GetComponent<SpiritsPassiveAbilities>();
        timeCounter = GameObject.FindGameObjectWithTag("Player").GetComponent<TimeCounter>();
        pickUp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PickUpObject>();
        //abyss = GameObject.FindGameObjectWithTag("Void").GetComponent<Void>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dataManager.puzzle3TimesOrdered = orderSystem.timesOrdered;
            dataManager.puzzle3TimesInvoked = spiritManager.timesInvoked;
            dataManager.puzzle3TimesMoved = spiritsPassive.timesMoved;
            dataManager.puzzle3TimePassed = timeCounter.timePassed;
            dataManager.puzzle3TimesPicked = pickUp.timesPicked;
            dataManager.puzzle3Deaths = abyss.timesDied;
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
