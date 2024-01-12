using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//attached to player
public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip runClip, walkClip;
    [SerializeField] float volMin, volMax, pitchMin, pitchMax;
    public static Action SetRunClipAction, SetWalkClipAction;

    private void OnEnable()
    {
        SetRunClipAction += SetRunClip;
        SetWalkClipAction += SetWalkClip;
    }
    private void OnDisable()
    {
        SetRunClipAction -= SetRunClip;
        SetWalkClipAction -= SetWalkClip;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void SetRunClip()
    {
        audioSource.clip = runClip;
        SetRandomVolume();
    }
    void SetWalkClip()
    {
        audioSource.clip = walkClip;
        SetRandomVolume();
    }

    void SetRandomVolume()
    {
        if (audioSource.isPlaying)
            return;
        audioSource.volume = UnityEngine.Random.Range(volMin, volMax);
        audioSource.pitch = UnityEngine.Random.Range(pitchMin, pitchMax);
        audioSource.Play();
    }
}
