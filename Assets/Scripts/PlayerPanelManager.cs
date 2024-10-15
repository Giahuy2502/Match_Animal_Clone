
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static DatabaseManager;

public class PlayerPanelManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] TextMeshProUGUI coinTxt;

    public static int Coin;
    
    void Start()
    {
        pausePanel.SetActive(false);
        Coin = PlayerPrefs.GetInt("coin",0);
        AdsManager.Instance.ShowBannerAd();
    }
    private void Update()
    {
        coinTxt.text = Coin.ToString();
    }
    public void OnPauseButton()
    {
        pausePanel.SetActive(true);
    }
    public void OnCoinButton()
    {
        StorePanelManager.IndexCurrentScene=SceneManager.GetActiveScene().buildIndex;
        Debug.Log("da bam va cua hang");
        SceneManager.LoadScene(2);
    }
    public void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
