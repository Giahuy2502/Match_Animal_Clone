using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoSingleton<AudioSourceManager>
{
    [SerializeField]private AudioSource effectSound;
    [SerializeField]private AudioClipManager audioClip;
    public static bool Soundable=true;
    protected override void DoOnAwake()
    {
        SetSoundable();
    }
    private static void SetSoundable()
    {
        int soundable = PlayerPrefs.GetInt("soundable", 1);
        Soundable = soundable == 1;
        Debug.Log(Soundable);
    }
    public void PlayAudio(int index)
    {
        Debug.Log($"----------(AudioSource) playsound +{Soundable}--------");
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
