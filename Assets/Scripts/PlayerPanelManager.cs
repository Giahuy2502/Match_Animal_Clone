
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPanelManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    void Start()
    {
        pausePanel.SetActive(false);
    }
    public void OnPauseButton()
    {
        pausePanel.SetActive(true);
    }
    public void OnCoinButton()
    {
        Debug.Log("da bam va cua hang");
        SceneManager.LoadScene(2);
    }
}
