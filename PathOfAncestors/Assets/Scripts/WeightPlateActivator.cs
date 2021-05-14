using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightPlateActivator : MonoBehaviour
{

    public int weight = 0;
    bool addPlayer = true;
    bool addBox= true;
    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Player" && addPlayer)
        {
            weight += 20;
            addPlayer = false;
            DataManager.totalTimesActivated++;

        }
        if (other.tag == "EARTH")
        {
            DataManager.totalTimesActivated++;
            weight += 10;

        }
        if (other.tag == "Box" && addBox)
        {
            DataManager.totalTimesActivated++;
            if (weight < 50)
            {
                weight += 70;
                addBox = false;
            }

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !addPlayer)
        {
            weight -= 20;
            addPlayer = true;
        }
        if (other.tag == "EARTH")
        {
            weight -= 10;

        }
        if (other.tag == "Box" && !addBox)
        {
            weight -= 70;
            addBox = true;
        }

    }
}
