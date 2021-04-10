using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUnparent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EarthPlatform")
        {
            this.transform.parent = other.transform;
        }
        if (other.tag == "Unparent")
        {
            this.transform.parent = null;
        }
    }
}
