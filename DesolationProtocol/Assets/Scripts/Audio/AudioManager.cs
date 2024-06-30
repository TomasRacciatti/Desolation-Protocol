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





    public void Awake()
    {
        Instance = this; 
    }
    public void Start()
    {
        //PlaySound("Music", "ambiente2");
    }

    public void PlaySound(string nameSource,string nameClip)
    {
       
        AudioSource source = SearchSource(nameSource);
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
