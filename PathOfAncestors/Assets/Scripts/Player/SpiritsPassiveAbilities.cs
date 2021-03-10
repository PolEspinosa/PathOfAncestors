using System.Collections;
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
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        //currentSpeed = walkSpeed;
        //windSpeed = walkSpeed * windSpeedMult;
        movingObject = null;
        //pushSpeed = walkSpeed * 0.5f;
        facedBox = false;
        inRange = false;
        pushing = false;
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
                if (earthActive && Input.GetKeyDown(KeyCode.E))
                {
                    pushing = !pushing;
                }
            }

            if (pushing)
            {
                MoveBox();
            }
            else
            {
                if (movingObject != null)
                {
                    movingObject.transform.parent = null;
                    movingObject = null;
                    gameObject.transform.LookAt(null);
                    facedBox = false;
                }
            }
        }
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

    private void MoveBox()
    {
        //if the player wasn't facing the cube, rotate the player so it is facing the cube
        if (!facedBox)
        {
            time = 0;

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
                gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, - 1);
            }
            //the player faces the left face of the box
            else if (facedDirection.x < -0.9)
            {
                gameObject.transform.localPosition = new Vector3(1, gameObject.transform.localPosition.y, 0);
            }
            //the player faces the left face of the box
            else if (facedDirection.x > 0.9)
            {
                gameObject.transform.localPosition = new Vector3( - 1, gameObject.transform.localPosition.y, 0);
            }
            gameObject.transform.parent = null;
        }
        ////delay to change parenting between moving object and player to avoid position problems
        //if (time < 0.05f)
        //{
        //    time += Time.deltaTime;
        //}
        //else
        //{
        //    movingObject.transform.parent = gameObject.transform;
        //}
    }
}
