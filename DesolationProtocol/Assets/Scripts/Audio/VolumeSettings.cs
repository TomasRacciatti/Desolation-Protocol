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

}
