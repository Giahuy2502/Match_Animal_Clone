using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanelManager : MonoBehaviour
{
    [SerializeField] TutorialPanelManager tutorialPanelManager;
    [SerializeField] Sprite muteImage;
    [SerializeField] Sprite unmuteImage;
    [SerializeField] Image muteButton;
    private ResourceManager ResourceManager=> ResourceManager.Instance;
    int indexScene;
    private void Start()
    {
        indexScene = SceneManager.GetActiveScene().buildIndex;
        UpdateMuteUI();
    }
    public void OnContinueButton()
    {
        this.gameObject.SetActive(false);
    }
    public void OnRestartButton()
    {
        SceneManager.LoadScene(indexScene);
    }
    public void OnHomeButton()
    {
        SceneManager.LoadScene("HomeScene");
    }
    public void OnLevelButton()
    {
        SceneManager.LoadScene("LevelScene");
    }
    public void OnTutorialButton()
    {
        tutorialPanelManager.gameObject.SetActive(true);
    }
    public void OnMuteButton()
    {
        AudioSourceManager.Soundable = !AudioSourceManager.Soundable;
        UpdateMuteUI();
        int soundable = AudioSourceManager.Soundable ? 1 : 0;
        PlayerPrefs.SetInt("soundable",soundable);
    }

    private void UpdateMuteUI()
    {
        if (AudioSourceManager.Soundable)
        {
            muteButton.sprite = unmuteImage;
        }
        else
        {
            muteButton.sprite = muteImage;
        }
    }

    public void OnVibrationButton()
    {
        ResourceManager.ResetUndoTool();
        ResourceManager.ResetMagnetTool();
        ResourceManager.ResetSortTool();
        ResourceManager.ResetCoin(0);
        PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
        PlayerPrefs.SetInt("undoCount", ResourceManager.GetUndoCount());
        PlayerPrefs.SetInt("magnetCount", ResourceManager.GetMagnetCount());
        PlayerPrefs.SetInt("sortCount", ResourceManager.GetSortCount());
    }
}
