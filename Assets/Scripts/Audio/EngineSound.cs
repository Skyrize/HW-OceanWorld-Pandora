using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    public float MinPitch = 1f;
    public float MaxPitch = 2f;
    [Min(0f)] public float MaxPitchAtSpeed = 42f;

    private Rigidbody m_body;
    private AudioSource m_audioSource;

    private void Awake()
    {
        m_body = GetComponent<Rigidbody>();
        m_audioSource = GetComponent<AudioSource>();

        if (MaxPitchAtSpeed == 0)
            MaxPitchAtSpeed = 999999f;
    }

    private void Update()
    {
        if (Time.timeScale == 0 && m_audioSource.isPlaying)
            m_audioSource.Pause();
        else if (Time.timeScale != 0 && !m_audioSource.isPlaying)
            m_audioSource.UnPause();

        var speed = m_body.velocity.magnitude;
        var pitch = Mathf.Lerp(MinPitch, MaxPitch, speed / MaxPitchAtSpeed);

        m_audioSource.pitch = pitch;
    }
}