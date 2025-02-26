using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingSettings : MonoBehaviour
{
    public static SavingSettings Instance { get; private set; }

    public bool isMusicOn;

    public int musicVolume;

    public int sensitivity;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveSettings()
    {
        // PlayerPrefs.SetInt("isMusicOn", isMusicOn ? 1 : 0);
        // PlayerPrefs.SetInt("musicVolume", musicVolume);
        // PlayerPrefs.SetInt("sensitivity", sensitivity);
    }

    public void LoadSettings()
    {
        // // Default values
        // isMusicOn = PlayerPrefs.GetInt("isMusicOn");
        // musicVolume = PlayerPrefs.GetInt("musicVolume");
        // sensitivity = PlayerPrefs.GetInt("sensitivity");
    }
}
