using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWaterSound : MonoBehaviour
{
    private FMOD.Studio.EventInstance waterSound;
    // Start is called before the first frame update
    void Start()
    {
        waterSound = FMODUnity.RuntimeManager.CreateInstance("event:/Props/waterSound");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(waterSound, gameObject.transform, gameObject.GetComponent<Rigidbody>());
        waterSound.setVolume(0.15f);
        waterSound.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
