using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsAudioScript : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;

    [SerializeField] private Slider sfxSlider;

    private bool isMuted;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("isMuted")){
            isMuted = PlayerPrefs.GetInt("isMuted") == 1 ? true : false;
        } else {
            isMuted = false;
        }
        if(PlayerPrefs.HasKey("MasterVolume") == false){
            PlayerPrefs.SetFloat("MasterVolume", 1);
        }
        if(PlayerPrefs.HasKey("SFXVolume") == false){
            PlayerPrefs.SetFloat("SFXVolume", 1);
        }
        setVolume(PlayerPrefs.GetFloat("MasterVolume"));  
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        AudioManager.instance.SetVolume(PlayerPrefs.GetFloat("MasterVolume")); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setVolume(float _volume)
    {
        if(_volume < 1){
            _volume = .001f;
        }
        refreshSlider(_volume);
        PlayerPrefs.SetFloat("MasterVolume", _volume);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(_volume/100) * 20);
    }

    public void refreshSlider(float _volume)
    {
        slider.value = _volume;
    }

    public void setVolumeFromSlider()
    {
        AudioManager.instance.SetVolume(slider.value);
        PlayerPrefs.SetFloat("MasterVolume", slider.value);
    }

    public void MuteAudio()
    {
        if(isMuted){
            AudioManager.instance.StopAudio();
            PlayerPrefs.SetFloat("MasterVolume", 0);
        } else {
            AudioManager.instance.PlayAudio("Background");
            setVolume(PlayerPrefs.GetFloat("MasterVolume"));
        }
        isMuted = !isMuted;
    }

    public void SfxMuteAudio()
    {
        if(!isMuted){
            AudioManager.instance.StopSFXAudio();
        } else {
            setVolume(0);
        }
        isMuted = !isMuted;
    }

    public void SetSFXVolume()
    {
        AudioManager.instance.SetSFXVolume(sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    public void backToMenu()
    {
        PlayerPrefs.SetFloat("MasterVolume", slider.value);
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        //PlayerPrefs.SetFloat("Sensitivity", );
        //PlayerPrefs.Save();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void backToGame()
    {
        PlayerPrefs.SetFloat("MasterVolume", slider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
    }
}
