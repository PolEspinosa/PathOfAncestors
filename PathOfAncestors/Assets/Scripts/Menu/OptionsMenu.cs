using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private FMOD.Studio.Bus masterMixer, musicMixer, ambienceMixer, sfxMixer;

    //volume variables
    private float masterVolume, musicVolume, ambienceVolume, SFXVolume;
    [SerializeField]
    private Slider masterSlider, musicSlider, ambienceSlider, sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        masterMixer = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        musicMixer = FMODUnity.RuntimeManager.GetBus("bus:/Master/music");
        ambienceMixer = FMODUnity.RuntimeManager.GetBus("bus:/Master/ambient");
        sfxMixer = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");

        masterVolume = GameManager.masterVolume;
        musicVolume = GameManager.musicVolume;
        ambienceVolume = GameManager.ambienceVolume;
        SFXVolume = GameManager.sfxVolume;

        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        ambienceSlider.value = ambienceVolume;
        sfxSlider.value = SFXVolume;

        masterMixer.setVolume(masterVolume);
        musicMixer.setVolume(musicVolume);
        ambienceMixer.setVolume(ambienceVolume);
        sfxMixer.setVolume(SFXVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume(float _volume)
    {
        masterMixer.setVolume(_volume);
        GameManager.masterVolume= _volume;
    }

    public void SetMusicVolume(float _volume)
    {
        musicMixer.setVolume(_volume);
        GameManager.musicVolume = _volume;
    }

    public void SetAmbienceVolume(float _volume)
    {
        ambienceMixer.setVolume(_volume);
        GameManager.ambienceVolume = _volume;
    }

    public void SetSFXVolume(float _volume)
    {
        sfxMixer.setVolume(_volume);
        GameManager.sfxVolume = _volume;
    }
}
