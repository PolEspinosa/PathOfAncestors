﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritsPassiveAbilities1 : MonoBehaviour
{
    public bool pushing;
    public GameObject movingObject; //object the player is currently moving
    private bool facedBox; //set the rotation of the player to always face the box if he is pushing it
    public bool inRange; //determines whether the player is in range
    private bool earthActive; //earth spirit invoked
    private float pushSpeed;
    public SpiritManager spiritManager;
    public Vector3 facedDirection;
    public float time = 0;
    public bool boxColliding;

    private RaycastHit hit;
    //
    [SerializeField]
    private BoxCollider boxCollider;
    //
    [SerializeField]
    private float boxZOffset;
    [SerializeField]
    private float boxYOffset;

    private Rigidbody boxRigidbody;
    public float parentTimeDelay;

    //indicate the direction the plyer picked the box from
    public enum PickingSide
    {
        LEFT,RIGHT,FRONT,BACK,NONE
    };
    public PickingSide side;

    public bool inDarkArea;

    //pushing box sound
    private FMOD.Studio.EventInstance pushBoxInstance;
    private FMOD.Studio.PLAYBACK_STATE state;
    private Rigidbody playerRigidbody;
    [SerializeField]
    private InteractionDetection interaction;
    
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
        boxColliding = false;
        boxCollider.enabled = false;
        inDarkArea = false;
        side = PickingSide.NONE;
        pushBoxInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Mecanismos/moveRock");
        pushBoxInstance.setVolume(0.5f);
        playerRigidbody = gameObject.GetComponent<Rigidbody>();

        PlayerData data = SaveSystem.LoadPlayerData();
        if (data != null)
        {
            Vector3 position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            gameObject.transform.position = position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //just for testing purposes
        if (Input.GetKeyDown(KeyCode.B))
        {
            SaveSystem.DeleteAllData();
        }
            //the player is close enough to move the box
            if (interaction.GetIsInRange())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pushing = !pushing;
                }
            }
            //if pushing, do MoveBox function
            if (pushing)
            {
                interaction.GetMovingObject().GetComponent<PlatformParent>().isParent = false;
                interaction.GetMovingObject().GetComponent<PlatformParent>().canParent = false;
                MoveBox();
                if(playerRigidbody.velocity.magnitude > 0.1f)
                {
                    pushBoxInstance.getPlaybackState(out state);
                    if(state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                    {
                        FMODUnity.RuntimeManager.AttachInstanceToGameObject(pushBoxInstance, interaction.GetMovingObject().transform, boxRigidbody);
                        pushBoxInstance.start();
                    }
                }
                else
                {
                    pushBoxInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
            }
            //if not pushing, erase all realtion between box and character
            else
            {
                if (interaction.GetMovingObject() != null && !interaction.GetMovingObject().GetComponent<PlatformParent>().isParent)
                {
                    pushBoxInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    boxRigidbody = interaction.GetMovingObject().GetComponent<Rigidbody>();
                    boxRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    interaction.GetMovingObject().transform.parent = null;
                    BoxCollider[] auxBox = interaction.GetMovingObject().GetComponents<BoxCollider>();
                    foreach (BoxCollider b in auxBox)
                    {
                        b.enabled = true;
                    }
                    boxRigidbody.isKinematic = false;
                    interaction.GetMovingObject().GetComponent<PlatformParent>().canParent = true;
                    boxRigidbody = null;
                    movingObject = null;
                    interaction.movingObject = null;
                    gameObject.transform.LookAt(null);
                    facedBox = false;
                    boxCollider.enabled = false;
                    side = PickingSide.NONE;
                }
            }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("DarkArea"))
    //    {
    //        inDarkArea = true;
    //    }
    //}

   

    private void MoveBox()
    {
        //if the player wasn't facing the cube, rotate the player so it is facing the cube
        if (!facedBox)
        {
            boxRigidbody = interaction.GetMovingObject().GetComponent<Rigidbody>();
            time = 0;

            facedBox = true;
            facedDirection = interaction.GetMovingObject().transform.position - gameObject.transform.position;
            gameObject.transform.parent = interaction.GetMovingObject().transform;
            //the player faces the front face of the box
            if (facedDirection.z < -0.9)
            {
                side = PickingSide.FRONT;
                gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, 1);
            }
            //the player faces the back face of the box
            else if (facedDirection.z > 0.9)
            {
                side = PickingSide.BACK;
                gameObject.transform.localPosition = new Vector3(0, gameObject.transform.localPosition.y, - 1);
            }
            //the player faces the right face of the box
            else if (facedDirection.x < -0.9)
            {
                side = PickingSide.RIGHT;
                gameObject.transform.localPosition = new Vector3(1, gameObject.transform.localPosition.y, 0);
            }
            //the player faces the left face of the box
            else if (facedDirection.x > 0.9)
            {
                side = PickingSide.LEFT;
                gameObject.transform.localPosition = new Vector3( - 1, gameObject.transform.localPosition.y, 0);
            }
            gameObject.transform.parent = null;
            //get the colliders of the moving object
            BoxCollider []auxBox = interaction.GetMovingObject().GetComponents<BoxCollider>();
            foreach(BoxCollider b in auxBox)
            {
                if (!b.isTrigger)
                {
                    //boxCollider.size = new Vector3(b.size.x * b.gameObject.transform.localScale.x, b.size.y * b.gameObject.transform.localScale.y, b.size.z * b.gameObject.transform.localScale.z);
                    boxCollider.center = new Vector3(0, Mathf.Abs(1 - boxCollider.size.y / 2f + boxYOffset), boxCollider.size.z / 2f + boxZOffset);
                    boxCollider.enabled = true;
                    b.enabled = false;  
                }
            }
            boxRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            boxRigidbody.isKinematic = true;
        }
        //delay to change parenting between moving object and player to avoid position problems
        if (time < parentTimeDelay)
        {
            time += Time.deltaTime;
        }
        else
        {
            interaction.GetMovingObject().transform.parent = gameObject.transform;
        }
    }

    private bool CanInteract()
    {
        if (movingObject != null)
        {
            Vector3 distance = movingObject.transform.position - gameObject.transform.position;
            Debug.Log(distance.y);
            return movingObject.GetComponent<Rigidbody>().velocity.y <= 0.1f && movingObject.GetComponent<Rigidbody>().velocity.y >= -0.1f;
            //return distance.y >= -0.5f && distance.y <= 1.2f;
        }
        else
        {
            return false;
        }
    }
}
