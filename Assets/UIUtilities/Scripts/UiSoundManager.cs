using System.Collections.Generic;
using UiUtilities;
using UnityEngine;
using UnityEngine.Serialization;

public class UiSoundManager : MonoSingleton<UiSoundManager>
{
    [SerializeField] private List<UISound> uiSounds;
    [SerializeField] private AudioSource backGroundAudio;
    private Queue<AudioSource> _audioSources;

    public Queue<AudioSource> AudioSources
    {
        get
        {
            if (_audioSources == null)
                _audioSources = new Queue<AudioSource>();
            return _audioSources;
        }
        private set => _audioSources = value;
    }

    protected override void DoOnAwake()
    {
        //base.Awake();
        uiSounds.Sort((a, b) => a.UISoundType - b.UISoundType);
    }

    public void PlayMusic(UISoundType type)
    {
       

        var uiSound = GetUiSound(type);
        backGroundAudio.clip = uiSound.audioClip;
      
        backGroundAudio.Play();
    }

    public void UpdateMusic(float volume)
    {
        backGroundAudio.volume = volume;
    }


    public void PlaySound(UISoundType type)
    {
        
        var uiSound = GetUiSound(type);
        var audioSource = GetAudioSource();
        audioSource.PlayOneShot(uiSound.audioClip);
    }

    private UISound GetUiSound(UISoundType type)
    {
        // return _uiSounds.Find(x => x.UISoundType == type).audioClip;
        return uiSounds[(int)type];
    }

    private AudioSource GetAudioSource()
    {
        AudioSource audioSource = null;
        if (AudioSources.Count > 0)
        {
            audioSource = AudioSources.Peek();
            if (audioSource == null)
            {
                AudioSources.Dequeue();
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    AudioSources.Enqueue(audioSource);
                    AudioSources.Dequeue();
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
            }
        }
        else
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        AudioSources.Enqueue(audioSource);
       
        return audioSource;
    }
}