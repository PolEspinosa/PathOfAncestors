using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritsPassiveAbilities : MonoBehaviour
{
    private bool pushing;
    private GameObject movingObject; //object the player is currently moving
    private bool facedBox; //set the rotation of the player to always face the box if he is pushing it
    private bool inRange; //determines whether the player is in range
    private float windSpeed;
    private bool windActive; //wind spirit invoked
    private bool earthActive; //earth spirit invoked
    public float windSpeedMult;
    private float pushSpeed;
    public SpiritManager spiritManager;
    // Start is called before the first frame update
    void Start()
    {
        //currentSpeed = walkSpeed;
        //windSpeed = walkSpeed * windSpeedMult;
        movingObject = null;
        //pushSpeed = walkSpeed * 0.5f;
        facedBox = false;
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spiritManager.currentSpirit != null)
        {
            if (spiritManager.currentSpirit.CompareTag("EARTH"))
            {
                earthActive = true;
                windActive = false;
            }
            else if (spiritManager.currentSpirit.CompareTag("WIND"))
            {
                windActive = true;
                earthActive = false;
            }
            //if neither the earth spirit nor the wind spirits are invoked
            else
            {
                earthActive = false;
                windActive = false;
            }
            //the player is close enough to move the box
            if (inRange)
            {
                if (earthActive && Input.GetKey(KeyCode.E))
                {
                    pushing = true;
                }
                else
                {
                    if (movingObject != null)
                    {
                        pushing = false;
                        movingObject.transform.parent = null;
                        movingObject = null;
                        gameObject.transform.LookAt(null);
                        facedBox = false;
                    }
                }
            }

            if (pushing)
            {
                MoveBox();
            }
        }
    }

    private void MoveBox()
    {
        //if the player wasn't facing the cube, rotate the player so it is facing the cube
        if (!facedBox)
        {
            facedBox = true;
            Vector3 facedDirection;
            facedDirection = movingObject.transform.position - gameObject.transform.position;
            gameObject.transform.parent = movingObject.transform;
            //the player faces the front face of the box
            if (facedDirection.z < -0.9)
            {
                gameObject.transform.localPosition = new Vector3(0, 0, 1);
            }
            //the player faces the back face of the box
            else if (facedDirection.z > 0.9)
            {
                gameObject.transform.localPosition = new Vector3(0, 0, -1);
            }
            //the player faces the left face of the box
            else if (facedDirection.x < -0.9)
            {
                gameObject.transform.localPosition = new Vector3(1, 0, 0);
            }
            //the player faces the left face of the box
            else if (facedDirection.x > 0.9)
            {
                gameObject.transform.localPosition = new Vector3(-1, 0, 0);
            }
            gameObject.transform.LookAt(new Vector3(movingObject.transform.position.x, gameObject.transform.position.y, movingObject.transform.position.z));
            gameObject.transform.parent = null;
        }
        //currentSpeed = pushSpeed;
        movingObject.transform.parent = gameObject.transform;
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
}
