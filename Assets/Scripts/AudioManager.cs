using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager amInstance;
    
    public Sound[] sounds;
    private void Awake()
    {
        if (amInstance == null)
        {
            amInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        foreach(Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.audioClip;
            s.audioSource.volume = s.volume;
        }    
    }

    public void Play(String name)
    {
        foreach(Sound s in sounds)
        {
            if(s.name == name)
            {

                if(s.name == "LaserSFX" || s.name == "MissileSFX" || s.name == "ButtonClick") s.audioSource.pitch = 1 + UnityEngine.Random.Range(-0.2f,0.2f);

                s.audioSource.PlayOneShot(s.audioClip);
                return;
            }
        }
    }

    public void PlayMusic(String name)
    {
        foreach(Sound s in sounds)
        {
            if(s.name == name)
            {
                if(s.audioSource.isPlaying) return;
                s.audioSource.loop = true;
                s.audioSource.Play();
                return;
            }
        }
    }

    public void Stop(String name)
    {
        foreach(Sound s in sounds)
        {
            if(s.name == name)
            {
                s.audioSource.Stop();
                return;
            }
        }
    }
}
