using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    [SerializeField]ToolByUIManager toolByUIManager;
    ResourceManager ResourceManager => ResourceManager.Instance;

    int indexScene;
    [SerializeField]GameObject panel;
    private void Start()
    {
        indexScene = SceneManager.GetActiveScene().buildIndex;
    }
    public void OnHomeButton()
    {
        SceneManager.LoadScene(0);
    }
    public void OnRestartButton()
    {

        SceneManager.LoadScene(indexScene);
    }
    public void OnFreeButton()
    {
        
        AdsManager.Instance.ShowRewardedlAd();
        RewardedAds.watchedEvent.AddListener(Continue);
        
    }
    public void OnBuyButton()
    {
        if (ResourceManager.GetCoin() < 300) return;
        ResourceManager.SetCoin(-300);
        PlayerPrefs.SetInt("coin", ResourceManager.GetCoin()); 
        Continue();
        DataGame.stateCurrentPlay = 0;
        
    }
    public void Continue()
    {
        for(int i = 0;i<3;i++)
        {
            toolByUIManager.OnUndoButton();
            ResourceManager.SetUndoTool(1);
          
        }
        toolByUIManager.OnSortingButton();
        ResourceManager.SetSortTool(1);

        ExitPanel();
    }
    public void ExitPanel()
    {
        panel.SetActive(false);
        GameUtility.Log(this, panel.activeSelf.ToString(), Color.blue);
    }
}
