﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance bgMusicInstance;
    private FMOD.Studio.PLAYBACK_STATE state;
    private bool isPlaying;
    private SpiritManager spiritManager;
    //we will get from here when the spirit is invoked
    // Start is called before the first frame update
    void Start()
    {
        bgMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/bgMusic");
        bgMusicInstance.setVolume(0.25f);
        bgMusicInstance.start();
        isPlaying = true;
        spiritManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SpiritManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bgMusicInstance.getPlaybackState(out state);
        if (Input.GetKeyDown(KeyCode.M))
        {
            isPlaying = !isPlaying;
        }
        if (isPlaying)
        {
            bgMusicInstance.setVolume(0.25f);
        }
        else
        {
            bgMusicInstance.setVolume(0);
        }
        //change the music depending on the invoked spirit
        bgMusicInstance.setParameterByName("fireSpiritInvoked", spiritManager.fireInvoked);
        bgMusicInstance.setParameterByName("earthSpiritInvoked", spiritManager.earthInvoked);
    }

    private void OnDestroy()
    {
        bgMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        bgMusicInstance.release();
    }
}