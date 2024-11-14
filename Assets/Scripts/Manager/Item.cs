using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI levelTxt;
    public Level levelData;
    [SerializeField] Button button;
    [SerializeField] Image unlockImg;
    [SerializeField] public List<Image> imageList= new List<Image>();
    UnlockLevel unlock =>UnlockLevel.Instance;
    AdsManager adsManager => AdsManager.Instance;

    private void OnEnable()
    {
        button.onClick.AddListener(OnClickButton);
    }
    private void Start()
    {
        UpdateUI();
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClickButton);
    }
    public void OnClickButton()
    {
        if (!unlock.unlockData.unlocked[level-1]) return;
        BoardManager.levelCurrent = level;
        InterstitialAd.WatchedAd.AddListener(LoadPlayScene);
        ShowAd();
    }
    void LoadPlayScene()
    {
        Debug.Log("--- (Item)(day la load ----- scene) ----");
        SceneManager.LoadScene("PlayScene");
    }
    public void ShowAd()
    {
        adsManager.ShowInterstitialAd();
        adsManager.LoadInterstitialAd();
    }
    public int GetStars(int level)
    {
        return PlayerPrefs.GetInt("Level_" + level + "_Stars", 0);
    }
    void FillStar()
    {
        int starFill= GetStars(level);

        if (starFill==0) return;
        for(int i = 0;i<starFill;i++)
        {
            imageList[i].enabled = true;
        }
    }
    void UpdateUI()
    {
        if (!unlock.unlockData.unlocked[level-1]) unlockImg.enabled = true;
        else
        {
            unlockImg.enabled = false;
            FillStar();
        }
    }
}