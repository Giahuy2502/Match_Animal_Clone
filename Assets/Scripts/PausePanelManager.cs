using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour
{
    private ToolManager ToolManager=> ToolManager.Instance;
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
        ToolManager.ResetUndoTool();
        ToolManager.ResetMagnetTool();
        ToolManager.ResetSortTool();
        PlayerPanelManager.Coin =0;
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
        PlayerPrefs.SetInt("undoCount", ToolManager.GetUndoCount());
        PlayerPrefs.SetInt("magnetCount", ToolManager.GetMagnetCount());
        PlayerPrefs.SetInt("sortCount", ToolManager.GetSortCount());
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
