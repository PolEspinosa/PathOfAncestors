﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritsPassiveAbilities : MonoBehaviour
{
    public bool pushing;
    public GameObject movingObject; //object the player is currently moving
    private bool facedBox; //set the rotation of the player to always face the box if he is pushing it
    private bool inRange; //determines whether the player is in range
    private bool earthActive; //earth spirit invoked
    private float pushSpeed;
    public SpiritManager spiritManager;
    public Vector3 facedDirection;
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
            }
            else
            {
                earthActive = false;
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
            facedDirection = movingObject.transform.position - gameObject.transform.position;
            gameObject.transform.parent = movingObject.transform;
            //the player faces the front face of the box
            if (facedDirection.z < -0.9)
            {
                gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, 1);
            }
            //the player faces the back face of the box
            else if (facedDirection.z > 0.9)
            {
                gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, -1);
            }
            //the player faces the left face of the box
            else if (facedDirection.x < -0.9)
            {
                gameObject.transform.localPosition = new Vector3(1, gameObject.transform.localPosition.y, 0);
            }
            //the player faces the left face of the box
            else if (facedDirection.x > 0.9)
            {
                gameObject.transform.localPosition = new Vector3(-1, gameObject.transform.localPosition.y, 0);
            }
            //gameObject.transform.LookAt(new Vector3(movingObject.transform.position.x, gameObject.transform.position.y, movingObject.transform.position.z));
            gameObject.transform.parent = null;
        }
        //currentSpeed = pushSpeed;
        //correct pivot position difference
        movingObject.transform.parent = gameObject.transform;
        //movingObject.transform.localPosition = new Vector3(movingObject.transform.localPosition.x, 0, movingObject.transform.localPosition.z);
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