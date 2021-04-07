using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance bgMusicInstance;
    //we will get from here when the spirit is invoked
    // Start is called before the first frame update
    void Start()
    {
        bgMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/bgMusic");
        bgMusicInstance.setVolume(0.2f);
        bgMusicInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
