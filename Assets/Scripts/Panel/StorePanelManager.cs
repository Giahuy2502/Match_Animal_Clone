using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;


public class StorePanelManager : MonoBehaviour
{
    public static int IndexCurrentScene;
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] TextMeshProUGUI undoTxt;
    [SerializeField] TextMeshProUGUI magTxt;
    [SerializeField] TextMeshProUGUI sortTxt;
    ResourceManager ResourceManager => ResourceManager.Instance;
    
    AdsManager adsManager => AdsManager.Instance;
    delegate void LoadAd();
    private void Start()
    {
        adsManager.ShowBannerAd();
    }
    private void Update()
    {
        UpdateUI();
    }
    public void OnExitButton()
    {
        SceneManager.UnloadSceneAsync(3);
    }
    
    void UpdateUI()
    {
        int coin = ResourceManager.GetCoin();
        int undo = ResourceManager.GetUndoCount();
        int mag = ResourceManager.GetMagnetCount();
        int sort = ResourceManager.GetSortCount();
        coinTxt.text = coin.ToString();
        undoTxt.text = undo.ToString();
        magTxt.text = mag.ToString();
        sortTxt.text = sort.ToString();
    }
    
}
