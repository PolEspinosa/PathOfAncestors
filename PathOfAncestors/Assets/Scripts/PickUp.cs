using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool hasObject = false;
    public GameObject objectInPossesion;
    public bool objectToPickUp = false;
    public bool inActivator = false;
    public GameObject activator;
    public GameObject player;
    public GameObject parent;
    float normalJump;
    float slowJump = 1;

    public GameObject cursor;
    Vector3 pos;
    Vector3 posUp;

    // Start is called before the first frame update
    void Start()
    {
        normalJump = player.GetComponent<CMF.AdvancedWalkerController1>().jumpSpeed;
        pos = cursor.transform.position;
        posUp = new Vector3(cursor.transform.position.x, cursor.transform.position.y+100, cursor.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectToPickUp && !hasObject)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                objectInPossesion.GetComponent<Rigidbody>().useGravity = false;
                objectInPossesion.GetComponent<Rigidbody>().isKinematic = true;
                objectInPossesion.GetComponent<Rigidbody>().detectCollisions = false;
                objectInPossesion.transform.position = parent.transform.position;
                objectInPossesion.transform.parent = parent.transform;
                parent.GetComponent<BoxCollider>().isTrigger = false;
                objectToPickUp = false;
                hasObject = true;


            }
        }

        else if (hasObject && !objectToPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!inActivator)
                {
                    objectInPossesion.GetComponent<Rigidbody>().useGravity = true;
                    objectInPossesion.GetComponent<Rigidbody>().isKinematic = false;
                    objectInPossesion.GetComponent<Rigidbody>().detectCollisions = true;
                    objectInPossesion.transform.parent = null;
                    hasObject = false;
                    parent.GetComponent<BoxCollider>().isTrigger = true;
                }
                else
                {
                    if (!activator.GetComponent<PlacingObjectActivator>()._activated)
                    {
                        objectInPossesion.transform.parent = null;
                        Destroy(objectInPossesion);
                        hasObject = false;
                        activator.GetComponent<PlacingObjectActivator>().ActiveOrb();
                        inActivator = false;
                        parent.GetComponent<BoxCollider>().isTrigger = true;
                    }
                    else
                    {
                        objectInPossesion.GetComponent<Rigidbody>().useGravity = true;
                        objectInPossesion.GetComponent<Rigidbody>().isKinematic = false;
                        objectInPossesion.GetComponent<Rigidbody>().detectCollisions = true;
                        objectInPossesion.transform.parent = null;
                        hasObject = false;
                        parent.GetComponent<BoxCollider>().isTrigger = true;
                    }

                }
            }

            
            CheckIfHasObject();



        }
    }


    void CheckIfHasObject()
    {
        if (hasObject)
        {
            player.GetComponent<CMF.AdvancedWalkerController1>().jumpSpeed = slowJump;
            cursor.transform.position = posUp;
        }
        else
        {
            player.GetComponent<CMF.AdvancedWalkerController1>().jumpSpeed = normalJump;
            cursor.transform.position = pos;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PickableObject" && !hasObject)
        {
            objectToPickUp = true;
            objectInPossesion = other.gameObject;
        }

        if (other.tag == "PlacingObject" && hasObject)
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
