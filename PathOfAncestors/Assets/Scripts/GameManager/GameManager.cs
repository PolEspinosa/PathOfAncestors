using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static float masterVolume, musicVolume, ambienceVolume, sfxVolume;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        LoadData();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.DeleteAll();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SaveData();
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("ambienceVolume", ambienceVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            masterVolume = PlayerPrefs.GetFloat("masterVolume");
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
            ambienceVolume = PlayerPrefs.GetFloat("ambienceVolume");
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            masterVolume = 1f;
            musicVolume = 1f;
            ambienceVolume = 1f;
            sfxVolume = 1f;
        }
    }
}
