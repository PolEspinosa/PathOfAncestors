using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFootsteps : MonoBehaviour
{
    //number of the surface
    private int iSurface;
    //tag of the surface
    private string sSurface;
    FMOD.Studio.EventInstance footstepsInstance;

    // Start is called before the first frame update
    void Start()
    {
        sSurface = "Piedra";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (sSurface != other.gameObject.tag)
        {
            switch (other.gameObject.tag)
            {
                case "piedra":
                    iSurface = 0;
                    break;
                case "metal":
                    iSurface = 1;
                    break;
                case "tierra":
                    iSurface = 2;
                    break;
            }
        }
    }

    //this function is trigger by the event in the walking animation
    void Footsteps()
    {
        footstepsInstance.start();
    }
}
