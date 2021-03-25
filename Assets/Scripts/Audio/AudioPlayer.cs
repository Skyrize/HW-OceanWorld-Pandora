using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioPlayer : MonoBehaviour
{
    private static GameObject s_root;

    private AudioManager m_manager;
    private AudioSource m_defaultSource;

    private void Awake()
    {
        if (!s_root)
        {
            s_root = new GameObject {name = "Sounds"};
        }

        m_manager = FindObjectOfType<AudioManager>();

        if (!m_manager)
            throw new NullReferenceException("Could not find the Audio Manager");

        m_defaultSource = m_manager.GetAudioSource();
    }

    public void PlaySound(string soundName)
    {
        var audioClip = m_manager.GetAudioClip(soundName);
        var soundObj = new GameObject {name = $"Sound {soundName}"};
        var source = soundObj.AddComponent<AudioSource>();

        soundObj.transform.SetParent(s_root.transform);
        soundObj.transform.position = transform.position;

        source.clip = audioClip.clip;
        source.volume = Random.Range(audioClip.minVolume, audioClip.maxVolume);
        source.pitch = Random.Range(audioClip.minPitch, audioClip.maxPitch);

        source.loop = audioClip.loop;
        source.priority = audioClip.priority;
        source.spatialBlend = m_defaultSource.spatialBlend;
        source.panStereo = m_defaultSource.panStereo;
        source.reverbZoneMix = m_defaultSource.reverbZoneMix;
        source.outputAudioMixerGroup = m_defaultSource.outputAudioMixerGroup;

        source.PlayOneShot(source.clip);
        Destroy(soundObj, audioClip.clip.length);
    }
}