using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftPanelManager : MonoBehaviour
{
    [SerializeField] RewardedAds rewardedAds;

    
    public void OnExitButton()
    {
        this.gameObject.SetActive(false);
        rewardedAds.ExitPanel();
    }
    public void OnAdsButton()
    {

        //rewardedAds.ShowAd();
    }
}
