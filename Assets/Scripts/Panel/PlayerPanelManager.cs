using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerPanelManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] TextMeshProUGUI coinTxt;

    ResourceManager ResourceManager=> ResourceManager.Instance;
    
    void Start()
    {
        pausePanel.SetActive(false);
        AdsManager.Instance.ShowBannerAd();
    }
    private void Update()
    {
        coinTxt.text = ResourceManager.GetCoin().ToString();
    }
    public void OnPauseButton()
    {
        pausePanel.SetActive(true);
    }
    public void OnCoinButton()
    {
        StorePanelManager.IndexCurrentScene=SceneManager.GetActiveScene().buildIndex;
        Debug.Log("da bam va cua hang");
        SceneManager.LoadSceneAsync(3,LoadSceneMode.Additive);
    }
    public void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
