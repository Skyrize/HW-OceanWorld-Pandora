using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioMixer mixer = null;

    public void SetVolume(string mixerName, float value)
    {
        if (value == 0) {
            mixer.SetFloat(mixerName, -80);
        } else {
            mixer.SetFloat(mixerName, Mathf.Log10(value) * 20 + 5);
        }
    }
    
    public void SetMasterVolume(float value)
    {
        SetVolume("MasterVolume", value);
    }
    
    public void SetEffectsVolume(float value)
    {
        SetVolume("EffectsVolume", value);
    }
    
    public void SetMusicVolume(float value)
    {
        SetVolume("MusicVolume", value);
    }
}
