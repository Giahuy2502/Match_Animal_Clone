using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorePanelManager : MonoBehaviour
{
    public static int IndexCurrentScene;
    [SerializeField] TextMeshProUGUI coinTxt;
    ToolManager ToolManager => ToolManager.Instance;
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
        PlayerPrefs.SetInt("coin",PlayerPanelManager.Coin);
        SceneManager.UnloadSceneAsync(2);
    }
    public void OnPriceButton()
    {
        ToolManager.SetUndoTool(15);
        ToolManager.SetMagnetTool(15);
        ToolManager.SetSortTool(15);
        PlayerPanelManager.Coin += 3000;
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
        PlayerPrefs.SetInt("undoCount",ToolManager.GetUndoCount());
        PlayerPrefs.SetInt("magnetCount", ToolManager.GetMagnetCount());
        PlayerPrefs.SetInt("sortCount", ToolManager.GetSortCount());
    }
    public void OnPriceButton1()
    {
        ToolManager.SetUndoTool(35);
        ToolManager.SetMagnetTool(35);
        ToolManager.SetSortTool(35);
        PlayerPanelManager.Coin += 7000;
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
        PlayerPrefs.SetInt("undoCount", ToolManager.GetUndoCount());
        PlayerPrefs.SetInt("magnetCount", ToolManager.GetMagnetCount());
        PlayerPrefs.SetInt("sortCount", ToolManager.GetSortCount());
    }
    public void OnPriceButton2()
    {
        PlayerPanelManager.Coin += 5000;
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
    }
    public void OnPriceButton3()
    {
        PlayerPanelManager.Coin += 15000;
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
    }
    public void OnPriceButton4()
    {
        PlayerPanelManager.Coin += 35000;
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
    }
    public void OnPriceButton5()
    {
        AdsManager.Instance.ShowRewardedlAd();
        RewardedAds.watchedEvent.AddListener(GetFreeCoin);
    }

    private static void GetFreeCoin()
    {
        PlayerPanelManager.Coin += 300;
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
    }
}
