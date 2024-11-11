using UnityEngine;

public class ProductButton : MonoBehaviour
{
    [SerializeField] private int coin;
    [SerializeField] private int undo;
    [SerializeField] private int magnet;
    [SerializeField] private int sort;
    ResourceManager ResourceManager => ResourceManager.Instance;
    AdsManager adsManager => AdsManager.Instance;
    AudioSourceManager audioSourceManager => AudioSourceManager.Instance;
    public void OnClickBuyButton()
    {
        ResourceManager.SetProduct(coin, undo, magnet, sort);
    }
    public void OnClickAdsButton()
    {
        AdsManager.Instance.ShowRewardedlAd();
        RewardedAds.watchedEvent.AddListener(GetFreeCoin);
    }
    private void GetFreeCoin()
    {
        ResourceManager.SetCoin(300);
        adsManager.LoadRewardedlAd();
    }
    public void OnSoundButton(int index)
    {
        audioSourceManager.PlayAudio(index);
    }
}
