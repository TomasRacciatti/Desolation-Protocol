using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer Mixer;
    public AudioClip[] AudioClips;
    public AudioMixerGroup[] audioMixerGroups;

    public AudioSource[] Sources;

    public static AudioManager Instance;


    /*[SerializeField] private AudioMixerSnapshot Paused, Unpaused;

    [SerializeField] private bool paused;*/


    public void Awake()
    {
        Instance = this; 
    }
    public void Start()
    {
        //Mixer.GetFloat("Master",out );
    }
    private void Update()
    {
        //agregar codigo de pausa 

        
    }

    public void PlaySound(string nameSource,string nameGroup,string nameClip)
    {
       
        AudioSource source = SearchSource(nameSource);
        AudioMixerGroup group = SearchGroup(nameGroup);
        AudioClip clip = SearchAudio(nameClip);
        if (clip != null)
        {
            source.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("No existe el sonido" + name);
        }
    }

    
    private AudioClip SearchAudio(string Name)
    {
        foreach (var clip in AudioClips)
        {
            if(clip.name == Name)
            {
                return clip;
            }
        }
       
        return null;
    }
    private AudioSource SearchSource(string Name)
    {
        foreach(var source in Sources)
        {
            if(source.name == Name)
            {
                return source;
            }
        }
        
        
        return null;
    }

    private AudioMixerGroup SearchGroup (string Name)
    {
        foreach (var Group in audioMixerGroups)
        {
            if (Group.name == Name)
            {
                return Group;
            }
        }


        return null;
    }
}
