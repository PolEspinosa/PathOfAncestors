using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPuzzle4 : MonoBehaviour
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
            dataManager.puzzle4TimesOrdered = orderSystem.timesOrdered;
            dataManager.puzzle4TimesInvoked = spiritManager.timesInvoked;
            dataManager.puzzle4TimesMoved = spiritsPassive.timesMoved;
            dataManager.puzzle4TimePassed = timeCounter.timePassed;
            dataManager.puzzle4TimesPicked = pickUp.timesPicked;
            dataManager.puzzle4Deaths = abyss.timesDied;
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
