using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFootsteps : MonoBehaviour
{
    //reference to the movement script
    [SerializeField]
    private CMF.AdvancedWalkerController inputManager;
    //determines if the player has hit the ground
    private bool hitGround;
    //number of the surface
    private int iSurface;
    //tag of the surface
    private string sSurface;

    private FMOD.Studio.EventInstance footstepsInstance;
    private FMOD.Studio.EventInstance groundHitInstance;

    // Start is called before the first frame update
    void Start()
    {
        sSurface = "Rock";
        hitGround = true;
        footstepsInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Player/playerFootsteps");
        footstepsInstance.setVolume(0.5f);
        groundHitInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Player/hitGround");
        groundHitInstance.setVolume(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //when the player hits the ground, if he had'nt previously hit the ground, hit ground = true
        if (!hitGround && inputManager.onGround)
        {
            HitGroundSound();
        }
        //when the player is not touching the ground, the player has not hit the ground
        else if (!inputManager.onGround)
        {
            hitGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("ReverbChange"))
        {
            //change surface sound according to the surface tag
            if (sSurface != other.gameObject.tag)
            {
                switch (other.gameObject.tag)
                {
                    case "Rock":
                        iSurface = 0;
                        break;
                    //case "Metal":
                    //    iSurface = 1;
                    //    break;
                    case "EarthPlatform":
                        iSurface = 2;
                        break;
                    case "EarthActivator":
                        iSurface = 2;
                        break;
                    default:
                        iSurface = 0;
                        break;
                }
                sSurface = other.gameObject.tag;
                footstepsInstance.setParameterByName("Surface Type", iSurface);
                groundHitInstance.setParameterByName("Surface Type", iSurface);
            }
        }
    }

    //this function is triggered by the event in the walking animation
    private void Footsteps()
    {
        if (inputManager.onGround)
        {
            footstepsInstance.start();
        }
    }

    private void HitGroundSound()
    {
        groundHitInstance.start();
        hitGround = true;
    }
}
