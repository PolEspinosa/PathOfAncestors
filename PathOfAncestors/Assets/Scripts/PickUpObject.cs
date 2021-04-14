using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public bool hasObject=false;
    public GameObject objectInPossesion;
    public bool objectToPickUp = false;
    public bool inActivator=false;
    public GameObject activator;
    public GameObject player;
    float normalJump;
    float slowJump = 3;

    public float timesPicked;

    private void Start()
    {
        normalJump = player.GetComponent<CMF.AdvancedWalkerController>().jumpSpeed;
    }



    private void Update()
    {
        DataManager.objectPicked = hasObject;
        if(objectToPickUp && !hasObject)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DataManager.totalTimesInteracted++;

                objectInPossesion.GetComponent<Rigidbody>().useGravity = false;
                objectInPossesion.GetComponent<Rigidbody>().isKinematic = true;
                objectInPossesion.GetComponent<Rigidbody>().detectCollisions = false;
                objectInPossesion.transform.position = this.transform.position;
                objectInPossesion.transform.parent = this.transform;
                objectToPickUp = false;
                hasObject = true;
                
            }
        }

        else if(hasObject && !objectToPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(!inActivator)
                {
                    objectInPossesion.GetComponent<Rigidbody>().useGravity = true;
                    objectInPossesion.GetComponent<Rigidbody>().isKinematic = false;
                    objectInPossesion.GetComponent<Rigidbody>().detectCollisions = true;
                    objectInPossesion.transform.parent = null;
                    hasObject = false;
                }
                else
                {
                    if(!activator.GetComponent<PlacingObjectActivator>()._activated)
                    {
                        objectInPossesion.transform.parent = null;
                        Destroy(objectInPossesion);
                        hasObject = false;
                        activator.GetComponent<PlacingObjectActivator>().ActiveOrb();
                        inActivator = false;
                    }
                    else
                    {
                        objectInPossesion.GetComponent<Rigidbody>().useGravity = true;
                        objectInPossesion.GetComponent<Rigidbody>().isKinematic = false;
                        objectInPossesion.GetComponent<Rigidbody>().detectCollisions = true;
                        objectInPossesion.transform.parent = null;
                        hasObject = false;
                    }
                   
                }
            }

            CheckIfHasObject();
                

            
        }
        
    }
    void CheckIfHasObject()
    {
        if(hasObject)
        {
            player.GetComponent<CMF.AdvancedWalkerController>().jumpSpeed = slowJump;
        }
        else
        {
            player.GetComponent<CMF.AdvancedWalkerController>().jumpSpeed = normalJump;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="PickableObject" &&!hasObject)
        {
            objectToPickUp = true;
            objectInPossesion = other.gameObject;
        }

        if(other.tag=="PlacingObject" && hasObject)
        {
            inActivator = true;
            activator = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickableObject" && !hasObject && objectToPickUp)
        {
            objectToPickUp = false;
            objectInPossesion = null;
        }
        if (other.tag == "PlacingObject" && hasObject)
        {
            inActivator = false;
            activator = null;
        }
    }


}
