using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Button Button;
    AdsManager adsManager => AdsManager.Instance;
    public  void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OnPlayButton()
    {
        Debug.Log(SpinManager.checkable);
        if (SpinManager.checkable)
        {
            Debug.Log("--- (day la load spin scene) ----");
            LoadScene();

        }
        else
        {
            Debug.Log("--- (day la load play scene) ----");
            InterstitialAd.WatchedAd.AddListener(LoadPlayScene);
            ShowAd();
        }
    }
    void LoadPlayScene()
    {
        Debug.Log("--- (day la load ----- scene) ----");
        SceneManager.LoadScene("PlayScene");
        
    }
    public void ShowAd()
    {
        adsManager.ShowInterstitialAd();
        adsManager.LoadInterstitialAd();
    }
}