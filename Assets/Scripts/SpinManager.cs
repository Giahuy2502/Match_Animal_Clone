using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Net.Http.Headers;

public class SpinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] TextMeshProUGUI undoTxt;
    [SerializeField] TextMeshProUGUI magTxt;
    [SerializeField] TextMeshProUGUI sortTxt;
    [SerializeField] Image spin;
    [SerializeField] Button exit;
    [SerializeField] Button ad;
    ToolManager ToolManager=> ToolManager.Instance;
    public static Vector3 corner;
    private void Start()
    {
        AdsManager.Instance.ShowBannerAd();
        InitValue();
        InitUI();
        
    }
    void InitValue()
    {
        PlayerPanelManager.Coin= PlayerPrefs.GetInt("coin", 0);
        ToolManager.ResetUndoTool(PlayerPrefs.GetInt("undoCount", 0));
        ToolManager.ResetMagnetTool(PlayerPrefs.GetInt("magnetCount", 0));
        ToolManager.ResetSortTool(PlayerPrefs.GetInt("sortCount", 0));
    }
    public void OnExitButton()
    {
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
        PlayerPrefs.SetInt("undoCount", ToolManager.GetUndoCount());
        PlayerPrefs.SetInt("magnetCount", ToolManager.GetMagnetCount());
        PlayerPrefs.SetInt("sortCount", ToolManager.GetSortCount());
        SceneManager.LoadScene(1);
    }
    public void OnAdButton()
    {
        AdsManager.Instance.ShowRewardedlAd();
        UnityEvent rotateSpin = RewardedAds.watchedEvent;
        rotateSpin.AddListener(RotateSpin);
    }
    void RotateSpin()
    {
        GameUtility.Log(this, "da xem quang cao de quay spin", Color.magenta);
        int z = Random.Range(1800, 2160);
        corner = new Vector3(0, 0, z);
        RectTransform spinRect = spin.rectTransform;
        spinRect.DORotate(corner, 5f, RotateMode.FastBeyond360);
        UpdateValue(z);
        ad.interactable = false;
        Invoke("UpdateUI", 5f);
        
    }
    void InitUI()
    {
        int coin = PlayerPrefs.GetInt("coin", 0);
        int undo = PlayerPrefs.GetInt("undoCount", 0);
        int mag = PlayerPrefs.GetInt("magnetCount", 0);
        int sort = PlayerPrefs.GetInt("sortCount", 0);
        coinTxt.text = coin.ToString();
        undoTxt.text = undo.ToString();
        magTxt.text = mag.ToString();
        sortTxt.text = sort.ToString();
    }
    void UpdateUI()
    {
        int coin = PlayerPanelManager.Coin;
        int undo = ToolManager.GetUndoCount();
        int mag = ToolManager.GetMagnetCount();
        int sort = ToolManager.GetSortCount();
        coinTxt.text = coin.ToString();
        undoTxt.text = undo.ToString();
        magTxt.text = mag.ToString();
        sortTxt.text = sort.ToString();
        ad.interactable = true;
    }
    private void UpdateValue(int z)
    {
        int angle = z % 360 + 23;
        int indexgift = (angle / 45)%8;
        Debug.Log($"indexgift = {indexgift}");
        
    }

}