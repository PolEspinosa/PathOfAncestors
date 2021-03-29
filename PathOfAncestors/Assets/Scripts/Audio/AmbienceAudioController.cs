using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceAudioController : MonoBehaviour
{
    private FMOD.Studio.EventInstance indoorAmbienceInstance, outdoorAmbienceInstance;
    // Start is called before the first frame update
    void Start()
    {
        //create indoor ambience instance
        //indoorAmbienceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Ambiente/indoorAmbience");
        //indoorAmbienceInstance.start();
        //create outdoor ambience instance
        //outdoorAmbienceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Ambiente/outdoorAmbience");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
