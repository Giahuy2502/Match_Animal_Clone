using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using System.Net.Http.Headers;
using Assets.SimpleLocalization.Scripts;

public class SpinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] TextMeshProUGUI undoTxt;
    [SerializeField] TextMeshProUGUI magTxt;
    [SerializeField] TextMeshProUGUI sortTxt;
    [SerializeField] Image spin;
    [SerializeField] Button exit;
    [SerializeField] Button ad;
    [SerializeField] SpinReward rewards;
    ResourceManager ResourceManager=> ResourceManager.Instance;
    AudioSourceManager audioSourceManager => AudioSourceManager.Instance;
    public static Vector3 corner;
    private void Start()
    {
        AdsManager.Instance.ShowBannerAd();
        //InitValue();
        InitUI();
        Debug.Log(LocalizationManager.Language);
    }
    //void InitValue()
    //{
    //    ResourceManager.ResetCoin(PlayerPrefs.GetInt("coin", 0));
    //    ResourceManager.ResetUndoTool(PlayerPrefs.GetInt("undoCount", 0));
    //    ResourceManager.ResetMagnetTool(PlayerPrefs.GetInt("magnetCount", 0));
    //    ResourceManager.ResetSortTool(PlayerPrefs.GetInt("sortCount", 0));
    //}
    public void OnExitButton()
    {
        PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
        PlayerPrefs.SetInt("undoCount", ResourceManager.GetUndoCount());
        PlayerPrefs.SetInt("magnetCount", ResourceManager.GetMagnetCount());
        PlayerPrefs.SetInt("sortCount", ResourceManager.GetSortCount());
        SceneManager.LoadScene(1);
    }
    public void OnAdButton()
    {
        AdsManager.Instance.ShowRewardedlAd();
        UnityEvent rotateSpin = RewardedAds.watchedEvent;
        rotateSpin.AddListener(RotateSpin);
    }
    public void OnSoundButton(int index)
    {
        audioSourceManager.PlayAudio(index);
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
        exit.interactable=false;    
        Invoke("UpdateUI", 5f);
        audioSourceManager.PlayAudio(3);

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
        int coin = ResourceManager.GetCoin();
        int undo = ResourceManager.GetUndoCount();
        int mag = ResourceManager.GetMagnetCount();
        int sort = ResourceManager.GetSortCount();
        coinTxt.text = coin.ToString();
        undoTxt.text = undo.ToString();
        magTxt.text = mag.ToString();
        sortTxt.text = sort.ToString();
        ad.interactable = true;
        exit.interactable = true;
    }
    private void UpdateValue(int z)
    {
        int angle = z % 360 + 23;
        int indexgift = (angle / 45)%8;
        Debug.Log($"indexgift = {indexgift}");
        GiftReward gift = rewards.Rewards[indexgift];
        int count= gift.GetCount();
        GiftType type = gift.GetGiftType();
        ResourceManager.SetTypeItem(type,count);

    }

}
