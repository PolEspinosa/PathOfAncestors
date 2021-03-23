using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParent : MonoBehaviour
{
    public bool onPlatform;
    public bool canParent = true;
    public GameObject platform;
    public bool isParent=false;

    public GameObject rayStart;
    RaycastHit hit;

    private void Update()
    {
        if(transform.parent==null)
        {
            if(canParent && onPlatform)
            {
                transform.parent = platform.transform;
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                isParent = true;
            }
        }
        if (Physics.Raycast(rayStart.transform.position, -rayStart.transform.up, out hit, 1))
        {
            if (hit.transform.tag == "EarthPlatform")
            {
                if (hit.transform.name == "EarthPlatform")
                {
                    onPlatform = true;
                    platform = hit.transform.gameObject.transform.GetChild(1).gameObject;
                }
                else if (hit.transform.name == "PlatformActivable")
                {
                    onPlatform = true;
                    platform = hit.transform.gameObject;
                }
            }

            else if (hit.transform.tag == "MovingPlatform")
            {
                onPlatform = true;
                platform = hit.transform.gameObject;
            }
            else
            {
                onPlatform = false;
                platform = null;
            }

        }
        else
        {
            onPlatform = false;
            platform = null;
        }

    }
}
   



