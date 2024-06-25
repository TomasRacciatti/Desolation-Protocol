using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider master;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sfx;
    [SerializeField] private Slider player;

    /*private void Start()
    {
        master.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        music.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sfx.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        player.value = PlayerPrefs.GetFloat("PlayerVolume", 0.75f);


        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetPlayerVolume();
    }*/

    public void SetMasterVolume()
    {
        float volumeMa = master.value;
        audioMixer.SetFloat("Master", volumeMa);
        //PlayerPrefs.SetFloat("MasterVolume", volumeMa);
    }

    public void SetMusicVolume()
    {
        float volumeMu = music.value;
        audioMixer.SetFloat("Music", volumeMu);
        //PlayerPrefs.SetFloat("MusicVolume", volumeMu);
    }

    public void SetSFXVolume()
    {
        float volumeS = sfx.value;
        audioMixer.SetFloat("SFX", volumeS);
        //PlayerPrefs.SetFloat("SFXVolume", volumeS);
    }

    public void SetPlayerVolume()
    {
        float volumeP = player.value;
        audioMixer.SetFloat("Player", volumeP);
        //PlayerPrefs.SetFloat("PlayerVolume", volumeP);
    }
}
