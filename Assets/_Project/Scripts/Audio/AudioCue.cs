using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCue : MonoBehaviour
{
    [SerializeField] private AudioCueEventChannelSO _audioCueEventChannel;

    public void PlayAudioCue(AudioCueSO audioCue)
    {
        _audioCueEventChannel.RaisePlayEvent(audioCue, transform.position);
    }
}
