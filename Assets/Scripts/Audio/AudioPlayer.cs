using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioPlayer : MonoBehaviour
{
    private static GameObject s_root;

    private AudioManager m_manager;

    private void Awake()
    {
        if (!s_root)
            s_root = new GameObject {name = "Sounds"};

        m_manager = FindObjectOfType<AudioManager>();

        if (!m_manager)
            throw new NullReferenceException("Could not find the Audio Manager");
    }

    public void PlaySound(string soundName)
    {
        if (!m_manager)
            m_manager = FindObjectOfType<AudioManager>();

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

        source.PlayOneShot(source.clip);
        Destroy(soundObj, audioClip.clip.length);
    }
}