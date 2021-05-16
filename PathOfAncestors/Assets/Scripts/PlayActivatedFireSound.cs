using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayActivatedFireSound : MonoBehaviour
{
    [SerializeField]
    private Activator activator;
    //create the event instance for the torch sound
    private FMOD.Studio.EventInstance torchSoundInstance;
    // Start is called before the first frame update
    void Start()
    {
        torchSoundInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Puerta 2/openBigDoor");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(torchSoundInstance, gameObject.transform, gameObject.GetComponent<Rigidbody>());
        //if (activator._activated)
        //{
        //    torchSoundInstance.start();
        //}
        
        torchSoundInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
