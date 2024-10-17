using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPanelManager : MonoBehaviour
{

    
    public void OnExitButton()
    {
        this.gameObject.SetActive(false);
        
    }
    public void OnAdsButton()
    {
        AdsManager.Instance.ShowRewardedlAd();
        RewardedAds.watchedEvent.AddListener(GetFreeTool);
        this.gameObject.SetActive(false);
    }
    void GetFreeTool()
    {
        ToolManager.undoCount++;
        ToolManager.sortCount++;
        ToolManager.magnetCount++;
        PlayerPrefs.SetInt("undoCount", ToolManager.undoCount);
        PlayerPrefs.SetInt("magnetCount", ToolManager.magnetCount);
        PlayerPrefs.SetInt("sortCount", ToolManager.sortCount);
    }
}
