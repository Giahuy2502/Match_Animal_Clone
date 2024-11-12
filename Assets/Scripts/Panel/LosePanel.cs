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
    VibrationController vibrationController => VibrationController.Instance;

    private int indexScene;
    private int indexSound;
    
    void SetIndexSound(int indexSound)
    {
         this.indexSound = indexSound;
    }
    private void Start()
    {
        vibrationController.StartVibration();
        indexScene = SceneManager.GetActiveScene().buildIndex;
        ShowAd();
    }
    public void ShowAd()
    {
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
        Debug.Log("-------***  xem quang cao   ***--------");
        SetIndexSound(0);
        adsManager.ShowRewardedlAd();
        RewardedAds.watchedEvent.AddListener(Continue);
        adsManager.LoadRewardedlAd();
    }
    public void OnBuyButton()
    {
        SetIndexSound(1);
        Debug.Log("-------***  300 xu   ***--------");
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
        Debug.Log("-------***  continue   ***--------");
        Undo3Cell();
        SortGrid();
        SetupState();
        ExitPanel();
    }

    private static void SetupState()
    {
        DataGame.stateCurrentPlay = 0;
    }

    private void SortGrid()
    {
        ResourceManager.SetSortTool(1);
        toolByUIManager.OnSortingButton();
    }

    private void Undo3Cell()
    {
        for (int i = 0; i < 3; i++)
        {

            ResourceManager.SetUndoTool(1);
            toolByUIManager.OnUndoButton();

        }
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
