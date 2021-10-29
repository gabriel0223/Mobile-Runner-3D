using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Listening on channels")]
    [SerializeField] private AudioCueEventChannelSO _SFXEventChannel;
    [SerializeField] private AudioCueEventChannelSO _musicEventChannel;

    private void OnEnable()
    {
        _SFXEventChannel.OnAudioCueRequested += PlayAudioCue;
        _musicEventChannel.OnAudioCueRequested += PlayAudioCue;
    }

    private void OnDisable()
    {
        _SFXEventChannel.OnAudioCueRequested -= PlayAudioCue;
        _musicEventChannel.OnAudioCueRequested -= PlayAudioCue;
    }

    private void PlayAudioCue(AudioCueSO audioCue, Vector3 position)
    {
        GameObject soundEmitter = ObjectPooler.Instance.GetObject(PoolableObjectTypes.SoundEmitter);
        soundEmitter.SetActive(true);
        soundEmitter.GetComponent<SoundEmitter>().PlayAudioClip(audioCue, position);
    }
}
