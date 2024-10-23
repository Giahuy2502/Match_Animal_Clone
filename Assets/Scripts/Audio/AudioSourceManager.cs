using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoSingleton<AudioSourceManager>
{
    [SerializeField]private AudioSource effectSound;
    [SerializeField]private AudioClipManager audioClip;
    public void PlayAudio(int index)
    {
        if(!effectSound.isPlaying)
        {
            AudioClip effect = audioClip.GetAudioClip(index);
            effectSound.PlayOneShot(effect);
            GameUtility.Log(this,"PlaySound",Color.red);
        }
    }
}
