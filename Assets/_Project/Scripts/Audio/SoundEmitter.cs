using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioCueSO audioCue, Vector3 position)
    {
        if (audioCue.audioClips.Length > 1)
        {
            int randomIndex = Random.Range(0, audioCue.audioClips.Length);
            _audioSource.clip = audioCue.audioClips[randomIndex];
        }
        else
        {
            _audioSource.clip = audioCue.audioClips[0];
        }
        
        _audioSource.volume = audioCue.volume;
        _audioSource.pitch = audioCue.pitch;
        _audioSource.loop = audioCue.loop;
        _audioSource.transform.position = position;
        _audioSource.Play();
    }
}
