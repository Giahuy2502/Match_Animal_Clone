using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    
    [SerializeField] ToolManager toolManager;

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
        Continue();
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
            toolManager.OnUndoButton();
            ToolManager.undoCount++;
            Debug.Log(ToolManager.undoCount);
        }
        toolManager.OnSortingButton();
        ToolManager.sortCount++;
        Debug.Log(ToolManager.sortCount);
        this.gameObject.SetActive(false);
        Debug.Log(this.gameObject.activeSelf);
    }
}
