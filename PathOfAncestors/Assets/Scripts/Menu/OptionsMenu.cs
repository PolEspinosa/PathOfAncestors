using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private FMOD.Studio.Bus musicMixer, ambienceMixer, sfxMixer;

    //volume variables
    private float masterVolume, musicVolume, ambienceVolume, SFXVolume;

    // Start is called before the first frame update
    void Start()
    {
        musicMixer = FMODUnity.RuntimeManager.GetBus("bus:/music");
        ambienceMixer = FMODUnity.RuntimeManager.GetBus("bus:/ambient");
        sfxMixer = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume(float _volume)
    {
        musicMixer.setVolume(_volume);
        ambienceMixer.setVolume(_volume);
        sfxMixer.setVolume(_volume);
    }

    public void SetMusicVolume(float _volume)
    {
        musicMixer.setVolume(_volume);
    }

    public void SetAmbienceVolume(float _volume)
    {
        ambienceMixer.setVolume(_volume);
    }

    public void SetSFXVolume(float _volume)
    {
        sfxMixer.setVolume(_volume);
    }
}
