using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Button Button;
    AdsManager adsManager => AdsManager.Instance;
    private void OnEnable()
    {
        Button.onClick.AddListener(LoadScene);
    }
    public  void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OnPlayButton()
    {
        if (SpinManager.checkable)
        {
            LoadScene();
        }
        else
        {
            InterstitialAd.WatchedAd.AddListener(LoadPlayScene);
            ShowAd();
        }
    }
    void LoadPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
    }
    public void ShowAd()
    {
        adsManager.ShowInterstitialAd();
        adsManager.LoadInterstitialAd();
    }
}