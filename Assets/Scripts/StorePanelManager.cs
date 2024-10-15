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
        SceneManager.LoadScene(IndexCurrentScene);
    }
    public void OnPriceButton()
    {
        ToolManager.undoCount += 15;
        ToolManager.magnetCount += 15;
        ToolManager.sortCount += 15;
        PlayerPanelManager.Coin += 3000;
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
        PlayerPrefs.SetInt("undoCount",ToolManager.undoCount);
        PlayerPrefs.SetInt("magnetCount",ToolManager.magnetCount);
        PlayerPrefs.SetInt("sortCount", ToolManager.sortCount);
    }
    public void OnPriceButton1()
    {
        ToolManager.undoCount += 35;
        ToolManager.magnetCount += 35;
        ToolManager.sortCount += 35;
        PlayerPanelManager.Coin += 7000;
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
        PlayerPrefs.SetInt("undoCount", ToolManager.undoCount);
        PlayerPrefs.SetInt("magnetCount", ToolManager.magnetCount);
        PlayerPrefs.SetInt("sortCount", ToolManager.sortCount);
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
        PlayerPanelManager.Coin += 300;
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
    }
}
