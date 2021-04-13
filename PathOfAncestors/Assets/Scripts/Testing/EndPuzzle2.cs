using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPuzzle2 : MonoBehaviour
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
    private Text steps2Text, deaths2Text, time2Text;
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
        steps2Text.text = (orderSystem.timesOrdered + spiritManager.timesInvoked + spiritsPassive.timesMoved + pickUp.timesPicked).ToString();
        deaths2Text.text = abyss.timesDied.ToString();
        time2Text.text = timeCounter.timePassed.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dataManager.puzzle2TimesOrdered = orderSystem.timesOrdered;
            dataManager.puzzle2TimesInvoked = spiritManager.timesInvoked;
            dataManager.puzzle2TimesMoved = spiritsPassive.timesMoved;
            dataManager.puzzle2TimePassed = timeCounter.timePassed;
            dataManager.puzzle2TimesPicked = pickUp.timesPicked;
            dataManager.puzzle2Deaths = abyss.timesDied;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dataManager.SaveData();
            Destroy(gameObject);
        }
    }
}
