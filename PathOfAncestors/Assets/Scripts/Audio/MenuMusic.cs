using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private FMOD.Studio.EventInstance menuMusicInstance;
    private bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        menuMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/menuMusic");
        menuMusicInstance.setVolume(0.2f);
        menuMusicInstance.start();
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isPlaying = !isPlaying;
        }
        if (isPlaying)
        {
            menuMusicInstance.setVolume(0.2f);
        }
        else
        {
            menuMusicInstance.setVolume(0);
        }
    }

    private void OnDestroy()
    {
        menuMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        menuMusicInstance.release();
    }
}
