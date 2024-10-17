using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    [SerializeField]ToolByUIManager toolByUIManager;
     ToolManager ToolManager => ToolManager.Instance;

    int indexScene;
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
        if (PlayerPanelManager.Coin < 300) return;
        PlayerPanelManager.Coin -= 300;
        PlayerPrefs.SetInt("coin",PlayerPanelManager.Coin); 
        Continue();
        DataGame.stateCurrentPlay = 0;
    }
    public void Continue()
    {
        for(int i = 0;i<3;i++)
        {
            toolByUIManager.OnUndoButton();
            ToolManager.SetUndoTool(1);
          
        }
        toolByUIManager.OnSortingButton();
        ToolManager.SetSortTool(1);
 
        this.gameObject.SetActive(false);
        Debug.Log(this.gameObject.activeSelf);
    }
}
