using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource, sfxAudioSource;
    public Sound[] audioClips, sfxAudioClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        PlayAudio("Background");
        audioSource.volume = PlayerPrefs.GetFloat("MasterVolume");   
        sfxAudioSource.volume = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void PlayAudio(String index)
    {
        Sound s = Array.Find(audioClips, sound => sound.name == index);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + index + " not found!");
            return;
        } else
        {
            audioSource.clip = s.clip;
            audioSource.Play();
        }
    }

    public void PlaySFXAudio(string index)
    {
        Sound s = Array.Find(sfxAudioClips, sound => sound.name == index);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + index + " not found!");
            return;
        }
        else
        {
            sfxAudioSource.PlayOneShot(s.clip);
        }
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public void StopSFXAudio()
    {
        sfxAudioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxAudioSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    

}
