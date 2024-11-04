using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    [SerializeField]ToolByUIManager toolByUIManager;
    ResourceManager ResourceManager => ResourceManager.Instance;
    AudioSourceManager audioSourceManager => AudioSourceManager.Instance;
    AdsManager adsManager => AdsManager.Instance;

    private int indexScene;
    private int indexSound;
    
    void SetIndexSound(int indexSound)
    {
         this.indexSound = indexSound;
    }
    private void Start()
    {
        indexScene = SceneManager.GetActiveScene().buildIndex;
        adsManager.ShowInterstitialAd();
        adsManager.LoadInterstitialAd();
    }
    public void OnHomeButton()
    {
        SceneManager.LoadScene("HomeScene");
    }
    public void OnRestartButton()
    {

        SceneManager.LoadScene(indexScene);
    }
    public void OnFreeButton()
    {
        SetIndexSound(0);
        adsManager.ShowRewardedlAd();
        RewardedAds.watchedEvent.AddListener(Continue);
        adsManager.LoadRewardedlAd();
    }
    public void OnBuyButton()
    {
        SetIndexSound(1);
        if (ResourceManager.GetCoin() < 300)
        {
            SetIndexSound(2);
            return;
        }
        
        ResourceManager.SetCoin(-300);
        PlayerPrefs.SetInt("coin", ResourceManager.GetCoin()); 
        Continue();
             
    }
    public void Continue()
    {
        for(int i = 0;i<3;i++)
        {
            
            ResourceManager.SetUndoTool(1);
            toolByUIManager.OnUndoButton();

        }
        ResourceManager.SetSortTool(1);
        toolByUIManager.OnSortingButton();
        
        DataGame.stateCurrentPlay = 0;
        ExitPanel();
    }
    public void ExitPanel()
    {
        gameObject.SetActive(false);
        GameUtility.Log(this, gameObject.activeSelf.ToString(), Color.blue);
    }
    public void SoundEffect()
    {
        audioSourceManager.PlayAudio(indexSound);
    }
}
