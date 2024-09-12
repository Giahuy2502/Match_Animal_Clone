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


    void Start()
    {
        PlayPanel.SetActive(true);
        PausePanel.SetActive(false);
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
        GiftPanel.SetActive(false);
    }

    
    void Update()
    {
        if(DataGame.stateCurrentPlay==1)
        {
            WinPanel.SetActive(true);
        }
        else if(DataGame.stateCurrentPlay==2)
        {
            LosePanel.SetActive(true);
        }
    }
}
