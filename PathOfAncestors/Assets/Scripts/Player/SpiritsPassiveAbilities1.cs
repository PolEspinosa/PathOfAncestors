using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritsPassiveAbilities1 : MonoBehaviour
{
    public bool pushing;
    public GameObject movingObject; //object the player is currently moving
    private bool facedBox; //set the rotation of the player to always face the box if he is pushing it
    private bool inRange; //determines whether the player is in range
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
    }

    // Update is called once per frame
    void Update()
    {
            //the player is close enough to move the box
            if (inRange)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pushing = !pushing;
                }
            }
            //if pushing, do MoveBox function
            if (pushing)
            {
                movingObject.GetComponent<PlatformParent>().isParent = false;
                movingObject.GetComponent<PlatformParent>().canParent = false;
                MoveBox();
            }
            //if not pushing, erase all realtion between box and character
            else
            {
                if (movingObject != null && !movingObject.GetComponent<PlatformParent>().isParent)
                {
                    boxRigidbody = movingObject.GetComponent<Rigidbody>();
                    boxRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    movingObject.transform.parent = null;
                    BoxCollider[] auxBox = movingObject.GetComponents<BoxCollider>();
                    foreach (BoxCollider b in auxBox)
                    {
                        b.enabled = true;
                    }
                    boxRigidbody.isKinematic = false;
                    movingObject.GetComponent<PlatformParent>().canParent = true;
                    boxRigidbody = null;
                    movingObject = null;
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
        //else if (other.CompareTag("DarkArea"))
        //{
        //    inDarkArea = false;
        //}
    }

    private void MoveBox()
    {
        //if the player wasn't facing the cube, rotate the player so it is facing the cube
        if (!facedBox)
        {
            boxRigidbody = movingObject.GetComponent<Rigidbody>();
            time = 0;

            facedBox = true;
            facedDirection = movingObject.transform.position - gameObject.transform.position;
            gameObject.transform.parent = movingObject.transform;
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
            BoxCollider []auxBox = movingObject.GetComponents<BoxCollider>();
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
            movingObject.transform.parent = gameObject.transform;
        }
    }
}
