using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

[System.Serializable]
public class Sound
{
    public String name;
    public AudioClip audioClip;

    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,2f)]
    public float pitch;

    [HideInInspector]
    public AudioSource audioSource;

}
