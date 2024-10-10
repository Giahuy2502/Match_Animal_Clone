using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeCanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] InterstitialAd interstitialAd;
    private void Start()
    {
        interstitialAd.LoadAd();
    }
    private void Update()
    {
        int coin = PlayerPrefs.GetInt("coin",0);
        coinTxt.text = coin.ToString();
    }
    public void OnPlayButton()
    {
        interstitialAd.ShowAd();
        SceneManager.LoadScene(1);
    }
    public void OnResponButton()
    {

    }

    public void OnLanguageButton()
    {

    }
    public void OnShopButton()
    {
        StorePanelManager.IndexCurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(2);
    }
    public void OnLevelsButton()
    {

    }
    public void OnCoinButton()
    {
        StorePanelManager.IndexCurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(2);
    }
    public void OnGiftButton()
    {

    }
}
