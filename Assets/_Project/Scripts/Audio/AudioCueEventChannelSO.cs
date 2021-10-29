using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/AudioCue Event Channel")]
public class AudioCueEventChannelSO : ScriptableObject
{
    public UnityAction<AudioCueSO, Vector3> OnAudioCueRequested;

    public void RaisePlayEvent(AudioCueSO audioCue, Vector3 positionInSpace)
    {
        OnAudioCueRequested?.Invoke(audioCue, positionInSpace);
    }
}
