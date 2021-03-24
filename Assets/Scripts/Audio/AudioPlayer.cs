using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioPlayer : MonoBehaviour
{
    private AudioManager m_manager;
    private AudioSource m_defaultSource;

    private void Awake()
    {
        m_manager = FindObjectOfType<AudioManager>();

        if (!m_manager)
            throw new NullReferenceException("Could not find the Audio Manager");

        m_defaultSource = m_manager.GetAudioSource();
    }

    public void PlaySound(string soundName)
    {
        var audioClip = m_manager.GetAudioClip(soundName);
        var source = gameObject.AddComponent<AudioSource>();

        source.clip = audioClip.clip;
        source.volume = Random.Range(audioClip.minVolume, audioClip.maxVolume);
        source.pitch = Random.Range(audioClip.minPitch, audioClip.maxPitch);
        
        source.priority = m_defaultSource.priority;
        source.spatialBlend = m_defaultSource.spatialBlend;
        source.panStereo = m_defaultSource.panStereo;
        source.reverbZoneMix = m_defaultSource.reverbZoneMix;
        source.outputAudioMixerGroup = m_defaultSource.outputAudioMixerGroup;

        source.PlayOneShot(source.clip);
        Destroy(source, audioClip.clip.length);
    }
}