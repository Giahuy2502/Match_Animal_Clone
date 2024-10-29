using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeCanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] GameObject multiLanguage;

    AudioSourceManager audioSourceManager => AudioSourceManager.Instance;
    private void Start()
    {
        AdsManager.Instance.ShowBannerAd();
    }
    
    private void Update()
    {
        int coin = PlayerPrefs.GetInt("coin",0);
        coinTxt.text = coin.ToString();
        
    }
    public void OnPlayButton()
    {
        BoardManager.levelCurrent = PlayerPrefs.GetInt("level", 1);
        //AdsManager.Instance.ShowInterstitialAd();
        SceneManager.LoadScene(3);
    }
    public void OnResponButton()
    {

    }

    public void OnLanguageButton()
    {
        multiLanguage.SetActive(true);
    }
    public void OnShopButton()
    {
        StorePanelManager.IndexCurrentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }
    public void OnLevelsButton()
    {
        SceneManager.LoadScene(5);
    }
    public void OnCoinButton()
    {
        StorePanelManager.IndexCurrentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }
    public void OnGiftButton()
    {

    }
    public void OnSoundButton(int index)
    {
        audioSourceManager.PlayAudio(index);
    }
}
