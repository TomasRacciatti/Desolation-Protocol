using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
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
    }

    public void SetMusicVolume()
    {
        float volumeMu = music.value;
        audioMixer.SetFloat("Music", volumeMu);
    }

    public void SetSFXVolume()
    {
        float volumeS = sfx.value;
        audioMixer.SetFloat("SFX", volumeS);
    }

    public void UpdateSliders()
    {
        float MasterV;
        audioMixer.GetFloat("Master", out MasterV);
        master.value = MasterV;

        float musicV;
        audioMixer.GetFloat("Music", out musicV);
        master.value = musicV;

        float sfxV;
        audioMixer.GetFloat("SFX", out sfxV);
        master.value = sfxV;
    }
}
