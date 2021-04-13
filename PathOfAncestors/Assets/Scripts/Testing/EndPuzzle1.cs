using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPuzzle1 : MonoBehaviour
{
    [SerializeField]
    private OrderSystem orderSystem;
    [SerializeField]
    private DataManager dataManager;
    [SerializeField]
    private SpiritManager spiritManager;
    [SerializeField]
    private SpiritsPassiveAbilities spiritsPassive;
    [SerializeField]
    private TimeCounter timeCounter;
    [SerializeField]
    private PickUpObject pickUp;
    [SerializeField]
    private Void abyss;

    [SerializeField]
    private Text steps1Text, deaths1Text, time1Text;
    // Start is called before the first frame update
    void Start()
    {
        //orderSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<OrderSystem>();
        //spiritManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SpiritManager>();
        //dataManager = GameObject.FindGameObjectWithTag("DataManager").GetComponent<DataManager>();
        //spiritsPassive = GameObject.FindGameObjectWithTag("Player").GetComponent<SpiritsPassiveAbilities>();
        //timeCounter = GameObject.FindGameObjectWithTag("Player").GetComponent<TimeCounter>();
        //pickUp = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PickUpObject>();
        //abyss = GameObject.FindGameObjectWithTag("Void").GetComponent<Void>();
    }

    // Update is called once per frame
    void Update()
    {
        steps1Text.text = (orderSystem.timesOrdered + spiritManager.timesInvoked + spiritsPassive.timesMoved + pickUp.timesPicked).ToString();
        deaths1Text.text = abyss.timesDied.ToString();
        time1Text.text = timeCounter.timePassed.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dataManager.puzzle1TimesOrdered = orderSystem.timesOrdered;
            dataManager.puzzle1TimesInvoked = spiritManager.timesInvoked;
            dataManager.puzzle1TimesMoved = spiritsPassive.timesMoved;
            dataManager.puzzle1TimePassed = timeCounter.timePassed;
            dataManager.puzzle1TimesPicked = pickUp.timesPicked;
            dataManager.puzzle1Deaths = abyss.timesDied;
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
