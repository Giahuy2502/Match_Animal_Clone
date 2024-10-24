using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour
{
    [SerializeField] TutorialPanelManager tutorialPanelManager;
    private ResourceManager ResourceManager=> ResourceManager.Instance;
    int indexScene;
    private void Start()
    {
        indexScene = SceneManager.GetActiveScene().buildIndex;
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
        SceneManager.LoadScene(0);
    }
    public void OnLevelButton()
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
    public void OnTutorialButton()
    {
        tutorialPanelManager.gameObject.SetActive(true);
    }
    public void OnMuteButton()
    {

    }
    public void OnVibrationButton()
    {

    }
}
