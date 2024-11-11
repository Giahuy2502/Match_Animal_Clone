using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPanelManager : MonoBehaviour
{
    private ResourceManager ResourceManager => ResourceManager.Instance;
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
        ResourceManager.SetUndoTool(1);
        ResourceManager.SetSortTool(1);
        ResourceManager.SetMagnetTool(1);
    }
}
