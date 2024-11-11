using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject PlayPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel;
    [SerializeField] GameObject GiftPanel;

    AudioSourceManager audioSourceManager => AudioSourceManager.Instance;
    void Start()
    {
        PlayPanel.SetActive(true);
        PausePanel.SetActive(false);
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
        GiftPanel.SetActive(false);
    }

    public void OnSoundButton(int index)
    {
        audioSourceManager.PlayAudio(index);
    }
    void Update()
    {
        switch(DataGame.stateCurrentPlay)
        {
            case 1:
                WinPanel.SetActive(true);
                break;
            case 2:
                LosePanel.SetActive(true);
                break;
            default:
                LosePanel.SetActive(false);
                WinPanel.SetActive(false);
                break;
        }
        
    }
}
