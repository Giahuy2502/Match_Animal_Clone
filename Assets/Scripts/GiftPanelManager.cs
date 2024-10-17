using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPanelManager : MonoBehaviour
{
    private ToolManager ToolManager => ToolManager.Instance;
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
        ToolManager.SetUndoTool(1);
        ToolManager.SetSortTool(1);
        ToolManager.SetMagnetTool(1);
        PlayerPrefs.SetInt("undoCount", ToolManager.GetUndoCount());
        PlayerPrefs.SetInt("magnetCount", ToolManager.GetMagnetCount());
        PlayerPrefs.SetInt("sortCount", ToolManager.GetSortCount());
    }
}
