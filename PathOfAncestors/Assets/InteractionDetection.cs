using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetection : MonoBehaviour
{
    private bool inRange;
    public GameObject movingObject;
    // Start is called before the first frame update
    void Start()
    {
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //if in range of a box
        if (other.CompareTag("Box"))
        {
            inRange = true;
            movingObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if the player has leaved the box
        if (other.CompareTag("Box"))
        {
            inRange = false;
        }
    }

    public bool GetIsInRange()
    {
        return inRange;
    }

    public GameObject GetMovingObject()
    {
        return movingObject;
    }
}
