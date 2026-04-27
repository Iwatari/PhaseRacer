using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    [SerializeField] private Car car;
    private AudioSource engineAudioSource;

    [SerializeField] private float pitchModifier;
    [SerializeField] private float volumeModifire;
    [SerializeField] private float rpmModifire;

    [SerializeField] private float basePitch = 1.0f;
    [SerializeField] private float baseVolume = 0.4f;

    private void Start()
    {
        engineAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        engineAudioSource.pitch = basePitch + pitchModifier * ((car.EngineRpm / car.EngineMaxRpm) * rpmModifire);
        engineAudioSource.volume = baseVolume + pitchModifier * (car.EngineRpm / car.EngineMaxRpm);
    }
}
