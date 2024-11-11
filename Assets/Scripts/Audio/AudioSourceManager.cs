using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoSingleton<AudioSourceManager>
{
    [SerializeField]private AudioSource effectSound;
    [SerializeField]private AudioClipManager audioClip;
    public static bool Soundable=true;
    public void PlayAudio(int index)
    {
        if (!Soundable) return;
        
        if (effectSound.isPlaying)
        {
            effectSound.Stop();
        }

        AudioClip effect = audioClip.GetAudioClip(index);
        effectSound.PlayOneShot(effect);

        GameUtility.Log(this, "PlaySound " +Soundable, Color.red);
    }
}
