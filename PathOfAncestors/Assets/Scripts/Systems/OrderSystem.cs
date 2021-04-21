﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{

    public LayerMask ignoreMask;
    bool aiming = false;
    public GameObject aimCursor;
    public GameObject cursorReference;

    public RaycastHit hit;
    private Ray ray;
    public Vector3 goToPosition;
    public SpiritManager spiritManager;
    public Camera camera;

    public bool isGoingToEarth = false;
    public GameObject activator;



    // Start is called before the first frame update
    void Start()
    {
        aimCursor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(1))
        {
            aiming = true;
            //Cursor.visible = true;
            aimCursor.SetActive(true);
        }
        //stop aiming
        else if (Input.GetMouseButtonUp(1))
        {
            aiming = false;
            //Cursor.visible = false;
            aimCursor.SetActive(false);
        }
        //cast the ray
        if (aiming && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(aimCursor.transform.position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreMask))
            {
                ManageOrders(hit);
            }
            //ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if (Physics.Raycast(Camera.main.transform.position, cursorReference.transform.position - Camera.main.transform.position, out hit, 100, ~ignoreMask))
            //{
            //    spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.point);
            //}
        }
        //if not aiming, make the spirit follow the player again
        else if (!aiming && Input.GetMouseButtonDown(0))
        {
            spiritManager.currentSpirit.GetComponent<BaseSpirit>().ReturnToPlayer();

            isGoingToEarth = false;
            activator = null;
            ManageActivators();
        }
    }

    public void ManageOrders(RaycastHit hit)
    {
        Debug.Log(hit.transform.tag);
        isGoingToEarth = false;
        activator = null;
        //get target tag
        spiritManager.currentSpirit.GetComponent<BaseSpirit>().targetTag = hit.transform.tag;
        //get target gameobject
        spiritManager.currentSpirit.GetComponent<BaseSpirit>().targetObject = hit.collider.gameObject;
        if (hit.transform.tag == "Untagged")
        {
            //apply y offset to the fire spirit so it doesn't go through the target
            if (spiritManager.currentSpirit.CompareTag("FIRE"))
            {
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(new Vector3(hit.point.x, hit.point.y + 0.75f, hit.point.z));
            }
            else
            {
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.point);
            }
            ManageActivators();

        }
        else if (hit.transform.CompareTag("MovingPlatform"))
        {
            //apply y offset to the fire spirit so it doesn't go through the target
            if (spiritManager.currentSpirit.CompareTag("FIRE"))
            {
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(new Vector3(hit.point.x, hit.point.y + 0.75f, hit.point.z));
            }
            else
            {
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.collider.gameObject.transform.position);
            }
            ManageActivators();
        }
        else if (hit.transform.tag == "Torch")
        {
            if (spiritManager.activatorObject == null)
            {
                Transform pos = hit.transform.GetChild(0).transform;
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
            }
            else
            {
                ManageActivators();
            }

        }
        else if (hit.transform.tag == "OvenActivator")
        {
            activator = hit.transform.gameObject.transform.GetChild(0).gameObject;
            if (spiritManager.activatorObject == null)
            {
                Transform pos = hit.transform.GetChild(0).transform;
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
            }
            else
            {
                ManageActivators();
            }

        }



        else if (hit.transform.tag == "EarthPlatform")
        {
            isGoingToEarth = true;
            activator = hit.transform.gameObject.transform.GetChild(0).gameObject;
            if (spiritManager.activatorObject == null)
            {
                Transform pos = hit.transform.GetChild(0).transform;
                //spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
                //apply y offset to the fire spirit so it doesn't go through the target
                if (spiritManager.currentSpirit.CompareTag("FIRE"))
                {
                    spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(new Vector3(pos.position.x, pos.position.y + 1, pos.position.z));
                }
                else
                {
                    spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
                }
            }
            else
            {
                ManageActivators();
                Transform pos = hit.transform.GetChild(0).transform;
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);

            }



        }


        else if (hit.transform.tag == "BreakableWall")
        {
            isGoingToEarth = true;
            if (spiritManager.activatorObject == null)
            {
                Transform pos = hit.transform.GetChild(0).transform;
                spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
            }
            else
            {
                ManageActivators();
            }

        }


        //else if (hit.transform.tag == "Oven")
        //{
        //    Transform pos = hit.transform.GetChild(0).transform;
        //    spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(pos.position);
        //}
        else if (hit.transform.CompareTag("Burnable"))
        {
            spiritManager.currentSpirit.GetComponent<BaseSpirit>().MoveTo(hit.point);
        }
    }

    void ManageActivators()
    {
        if (spiritManager.activatorObject != null)
        {
            if (spiritManager.activatorObject.gameObject.name == "Entry")
            {
                spiritManager.activatorObject.GetComponent<OvenActivator>().DeactivateOven();
            }
            else if (spiritManager.activatorObject.gameObject.name == "EarthPlatformActivator")
            {
                spiritManager.activatorObject.GetComponent<EarthPlatformActivator>().DeactivateEarthPlatform();
            }

        }
    }
}
