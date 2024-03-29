﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public struct Audio {
    public string name;
    public AudioClip clip;
    public bool loop;
    [Range(0, 256)] public int priority;
    [Range(0f, 1f)] public float minVolume;
    [Range(0f, 1f)] public float maxVolume;
    public float minPitch;
    public float maxPitch;
    
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    
    public static bool operator ==(Audio f1, Audio f2) { return f1.name == f2.name && f1.clip == f2.clip && f1.maxVolume == f2.maxVolume && f1.maxPitch == f2.maxPitch && f1.minVolume == f2.minVolume && f1.minPitch == f2.minPitch; }
    public static bool operator !=(Audio f1, Audio f2) { return !(f1==f2); }
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool randomizeVolume = true;
    [SerializeField] private bool randomizePitch = true;
    [Header("Audios")]
    [SerializeField]
    private List<Audio> audios = new List<Audio>();

    private AudioSource source = null;

    private void Awake() {
        source = GetComponent<AudioSource>();
    }

    public AudioClip GetClip(string clipName)
    {
        Audio audio = audios.Find((a) => a.name == clipName);
        
        if (audio.name == null) {
            Debug.LogException(new System.Exception("Can't find audio named " + clipName));
        }
        return audio.clip;
    }

    public float GetClipLength()
    {
        return source.clip.length;
    }

    public float GetClipLength(string clipName)
    {
        Audio audio = audios.Find((a) => a.name == clipName);
        
        if (audio.name == null) {
            Debug.LogException(new System.Exception("Can't find audio named " + clipName));
        }
        return audio.clip.length;
    }

    private void Play(Audio audio)
    {
        if (randomizePitch)
            source.pitch = Random.Range(audio.minPitch, audio.maxPitch);
        if (randomizeVolume)
            source.volume = Random.Range(audio.minVolume, audio.maxVolume);
        source.clip = audio.clip;
        source.PlayOneShot(audio.clip);

    }

    public void Play(int index)
    {
        Audio audio;

        if (index < 0 || index > audios.Count) {
            Debug.LogException(new System.Exception("Invalid index " + index));
        }
        audio = audios[index];
        Play(audio);
    }

    public void Play(AudioClip clip)
    {
        Audio audio = audios.Find((a) => a.clip == clip);
        
        if (audio.name == null) {
            Debug.LogException(new System.Exception("Can't find audio with clip " + clip.name));
            return;
        }
        Play(audio);
    }

    public void Play(string audioName)
    {
        Audio audio = audios.Find((a) => a.name == audioName);
        
        if (audio.name == null) {
            Debug.LogException(new System.Exception("Can't find audio named " + audioName));
            return;
        }
        Play(audio);
    }

    public Audio GetAudioClip(string audioName)
    {
        var result = audios.Find(a => a.name == audioName);

        if (result == null)
            throw new NullReferenceException($"Audio file {audioName} not found");
        return result;
    }

    public AudioSource GetAudioSource()
    {
        return source;
    }
}
