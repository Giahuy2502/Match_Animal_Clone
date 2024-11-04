using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeCanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] GameObject multiLanguage;
    [SerializeField] TextMeshProUGUI lvTxt;

    AudioSourceManager audioSourceManager => AudioSourceManager.Instance;
    private void Start()
    {
        BoardManager.levelCurrent = PlayerPrefs.GetInt("level", 1);
        lvTxt.text=BoardManager.levelCurrent.ToString();
        AdsManager.Instance.ShowBannerAd();
    }
    
    private void Update()
    {
        int coin = PlayerPrefs.GetInt("coin",0);
        coinTxt.text = coin.ToString();
        
    }
    public void OnPlayButton()
    {
        if(SpinManager.checkable)
        {
            SceneManager.LoadScene("SpinScene");
        }
        else
        {
            LoadPlayScene();
            AdsManager.Instance.ShowInterstitialAd();
            AdsManager.Instance.LoadInterstitialAd();
        }
    }
    void LoadPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
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

        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }
    public void OnLevelsButton()
    {
        SceneManager.LoadScene("LevelScene");
    }
    public void OnCoinButton()
    {
        StorePanelManager.IndexCurrentScene = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
    }
    public void OnGiftButton()
    {

    }
    public void OnSoundButton(int index)
    {
        audioSourceManager.PlayAudio(index);
    }
}
