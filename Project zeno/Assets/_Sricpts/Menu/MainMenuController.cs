using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// using System.Diagnostics;

public class MainMenuController : MonoBehaviour
{
    // add audio source
    AudioManager audioSource;

    public Slider SensitivitySlider;

    public float sensitivity;

    public bool isMuted;

    public int musicVolume;

    public Text sensitivityText;

    void Start()
    {
        audioSource = AudioManager.instance;
        audioSource.PlayAudio("Background");
        sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        musicVolume = PlayerPrefs.GetInt("MasterVolume");
        SensitivitySlider.value = sensitivity;
        sensitivityText.text = sensitivity.ToString();
    }

    public void pvpButtonHandler()
    {
        SceneManager.LoadSceneAsync("Terrain");
    }

    public void pvcButtonHandler()
    {
        SceneManager.LoadSceneAsync("Terrain");
    }

    public void settingsButtonHandler()
    {
        SceneManager.LoadSceneAsync("Settings");
    }

    public void quitButtonHandler()
    {
        Application.Quit();
    }

    public void backToMenu()
    {
        PlayerPrefs.SetFloat("MasterVolume", musicVolume);
        PlayerPrefs.SetFloat("sensitivity", sensitivity);
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void MuteAudio()
    {
        if(isMuted){
            audioSource.StopAudio();
        } else {
            audioSource.PlayAudio("Background");
            SetVolume(0);
        }
        isMuted = !isMuted;
    }

    public void SetSensitivity(float _sensitivity)
    {
        sensitivity = _sensitivity;
        sensitivityText.text = sensitivity.ToString();
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }

    public void SetVolume(float _volume)
    {
        musicVolume = (int)_volume;
    }

    public void backToGame()
    {
        PlayerPrefs.SetFloat("Sensitivity",sensitivity);
        //PlayerPrefs.Save();
        SceneManager.LoadScene("Terrain");
        // load ship data
        ShipManager.instance.LoadBattleShipData();
    }

    void Update()
    {
       SetSensitivity(SensitivitySlider.value);
    }
}