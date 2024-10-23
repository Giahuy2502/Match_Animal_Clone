using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipManager",menuName = "AudioClipManager")]
public class AudioClipManager : ScriptableObject
{
    [SerializeField]List<AudioClip> clipList;

    public AudioClip GetAudioClip(int index)
    {
        return clipList[index];
    }
}
