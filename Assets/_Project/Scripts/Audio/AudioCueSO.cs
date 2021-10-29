using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "newAudioCue", menuName = "Audio/Audio Cue")]
public class AudioCueSO: ScriptableObject
{
    public AudioClip[] audioClips;
    public bool loop;
    [Range(0f, 1f)] public float volume = 0.5f;
    [Range(-3f, 3f)] public float pitch = 1f;
}
