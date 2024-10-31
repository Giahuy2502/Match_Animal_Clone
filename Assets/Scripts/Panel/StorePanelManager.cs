using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StorePanelManager : MonoBehaviour
{
    public static int IndexCurrentScene;
    [SerializeField] TextMeshProUGUI coinTxt;
    ResourceManager ResourceManager => ResourceManager.Instance;
    AudioSourceManager audioSourceManager => AudioSourceManager.Instance;
    delegate void LoadAd();
    private void Start()
    {
        AdsManager.Instance.ShowBannerAd();
    }
    private void Update()
    {
        int coin = PlayerPrefs.GetInt("coin",0);
        coinTxt.text = coin.ToString();
    }
    public void OnExitButton()
    {
        PlayerPrefs.SetInt("coin",ResourceManager.GetCoin());
        SceneManager.UnloadSceneAsync(3);
    }
    public void OnPriceButton()
    {
        ResourceManager.SetUndoTool(15);
        ResourceManager.SetMagnetTool(15);
        ResourceManager.SetSortTool(15);
        ResourceManager.SetCoin(3000);
        PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
        PlayerPrefs.SetInt("undoCount",ResourceManager.GetUndoCount());
        PlayerPrefs.SetInt("magnetCount", ResourceManager.GetMagnetCount());
        PlayerPrefs.SetInt("sortCount", ResourceManager.GetSortCount());
    }
    public void OnPriceButton1()
    {
        ResourceManager.SetUndoTool(35);
        ResourceManager.SetMagnetTool(35);
        ResourceManager.SetSortTool(35);
        ResourceManager.SetCoin(7000);
        PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
        PlayerPrefs.SetInt("undoCount", ResourceManager.GetUndoCount());
        PlayerPrefs.SetInt("magnetCount", ResourceManager.GetMagnetCount());
        PlayerPrefs.SetInt("sortCount", ResourceManager.GetSortCount());
    }
    public void OnPriceButton2()
    {
        ResourceManager.SetCoin(5000);
        PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
    }
    public void OnPriceButton3()
    {
        ResourceManager.SetCoin(15000);
        PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
    }
    public void OnPriceButton4()
    {
        ResourceManager.SetCoin(35000);
        PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
    }
    public void OnPriceButton5()
    {
        AdsManager.Instance.ShowRewardedlAd();
        RewardedAds.watchedEvent.AddListener(GetFreeCoin);
    }
    public void OnSoundButton(int index)
    {
        audioSourceManager.PlayAudio(index);
    }
    private void GetFreeCoin()
    {
        ResourceManager.SetCoin(300);
        PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
    }
}
