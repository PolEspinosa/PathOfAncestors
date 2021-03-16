using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParent : MonoBehaviour
{
    public bool onPlatform;
    public bool canParent = true;
    public GameObject platform;
    public bool isParent=false;
    // Start is called before the first frame update

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
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="EarthPlatform")
        {
            onPlatform = true;
            platform = other.gameObject.transform.GetChild(1).gameObject ;
            
        }

        if(other.transform.tag=="MovingPlatform")
        {
            onPlatform = true;
            platform = other.gameObject; ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "EarthPlatform" )
        {
            onPlatform = false;
            platform = null;

        }

        if (other.transform.tag == "MovingPlatform")
        {
            onPlatform = false;
            platform = null;

        }
    }


}

