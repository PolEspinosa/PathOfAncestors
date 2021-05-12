using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFireTut : MonoBehaviour
{
    public GameObject fireTut;
    bool enter = false;
  

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player" && !enter)
        {
            fireTut.SetActive(true);
            enter = true;
        }
    }
}
