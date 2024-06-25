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

    /*[SerializeField] private float masterVol;
    [SerializeField] private float musicVol;
    [SerializeField] private float SFXVol;
    [SerializeField] private float playerVol;


    private void Start()
    {
        masterVol = PlayerPrefs.GetFloat("master");
        master.value = masterVol;
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("master",masterVol);
    }*/

    public void SetMasterVolume()
    {
        float volumeMa = master.value;
        audioMixer.SetFloat("Master",volumeMa);
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
    public void SetPlayerVolume()
    {
        float volumeP = player.value;

        audioMixer.SetFloat("Player", volumeP);
    }
}
