using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoSingleton<AdsManager>
{
    private AdsInitializer adsInitializer = new AdsInitializer();
    private InterstitialAd interstitialAd = new InterstitialAd();
    private RewardedAds rewardedAds = new RewardedAds();
    private BannerAd bannerAd = new BannerAd();

    protected override void DoOnAwake()
    {
        // Khởi tạo các quảng cáo

        adsInitializer.InitializeAds();
        interstitialAd.Init();
        bannerAd.Init();
        rewardedAds.Init();

        LoadInterstitialAd();
        LoadRewardedlAd();
        LoadBannerAd();

    }
    public void LoadInterstitialAd()
    {
        interstitialAd.LoadAd();
    }
    public void LoadRewardedlAd()
    {
        rewardedAds.LoadAd();
    }
    public void LoadBannerAd()
    {
        bannerAd.LoadBanner();
    }
    public void ShowInterstitialAd()
    {
        interstitialAd.ShowAd();
    }
    public void ShowRewardedlAd()
    {
        rewardedAds.ShowAd();
    }
    public void ShowBannerAd()
    {
        bannerAd.ShowBannerAd();
    }
}
