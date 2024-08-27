
using UnityEngine;
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
}
