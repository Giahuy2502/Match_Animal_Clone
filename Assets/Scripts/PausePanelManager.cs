using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour
{
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
        ResourceManager.SetCoin(0);
        PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
        PlayerPrefs.SetInt("undoCount", ResourceManager.GetUndoCount());
        PlayerPrefs.SetInt("magnetCount", ResourceManager.GetMagnetCount());
        PlayerPrefs.SetInt("sortCount", ResourceManager.GetSortCount());
    }
    public void OnTutorialButton()
    {

    }
    public void OnMuteButton()
    {

    }
    public void OnVibrationButton()
    {

    }
}
