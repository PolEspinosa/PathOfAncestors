using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFootsteps1 : MonoBehaviour
{
    //reference to the movement script
    [SerializeField]
    private CMF.AdvancedWalkerController1 inputManager;
    //number of the surface
    private int iSurface;
    //tag of the surface
    private string sSurface;

    // Start is called before the first frame update
    void Start()
    {
        sSurface = "Rock";
    }

    // Update is called once per frame
    void Update()
    {

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
                    case "Metal":
                        iSurface = 1;
                        break;
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
                inputManager.groundHitEventInstance.setParameterByName("Surface Type", iSurface);
                inputManager.footstepsEventInstance.setParameterByName("Surface Type", iSurface);
            }
        }
    }
}
