using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFootsteps : MonoBehaviour
{
    //reference to the movement script
    [SerializeField]
    private bool hitGround;
    //number of the surface
    private int iSurface;
    //tag of the surface
    private string sSurface;
    FMOD.Studio.EventInstance footstepsInstance;

    // Start is called before the first frame update
    void Start()
    {
        sSurface = "Rock";
        hitGround = false;
        footstepsInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Player/playerFootsteps");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(sSurface);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (sSurface != other.gameObject.tag)
        {
            switch (other.gameObject.tag)
            {
                case "Rock":
                    iSurface = 0;
                    break;
                case "Metal":
                    iSurface = 1;
                    break;
                case "EarthPlatform":
                    iSurface = 2;
                    break;
                default:
                    iSurface = 0;
                    break;
            }
            sSurface = other.gameObject.tag;
            footstepsInstance.setParameterByName("Surface Type", iSurface);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        
    }

    //this function is trigger by the event in the walking animation
    void Footsteps()
    {
        footstepsInstance.start();
        //Debug.Log("hello");
    }
}
