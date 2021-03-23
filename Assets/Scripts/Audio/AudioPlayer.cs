using System;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioManager m_manager;

    private void Awake()
    {
        m_manager = FindObjectOfType<AudioManager>();

        if (!m_manager)
            throw new NullReferenceException("Could not find the Audio Manager");
    }

    public void PlaySound(string soundName)
    {
        m_manager.Play(soundName);
    }
}