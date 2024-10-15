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
        this.gameObject.SetActive(false);
    }
}
