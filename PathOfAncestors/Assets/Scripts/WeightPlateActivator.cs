using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightPlateActivator : MonoBehaviour
{

    public int weight = 0;
    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == "Player")
        {
            weight += 20;

        }
        if (other.tag == "EARTH")
        {
            weight += 10;

        }
        if (other.tag == "Box")
        {
            if (weight < 50)
            {
                weight += 70;
            }

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            weight -= 20;

        }
        if (other.tag == "EARTH")
        {
            weight -= 10;

        }
        if (other.tag == "Box")
        {
            weight -= 70;
        }

    }
}
