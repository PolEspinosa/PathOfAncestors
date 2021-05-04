using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour
{
    public List<GameObject> objects;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            foreach(GameObject obj in objects)
            {
                obj.SetActive(true);
            }
        }
    }
}
