using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour
{
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
        ToolManager.undoCount =0;
        ToolManager.magnetCount =0;
        ToolManager.sortCount = 0;
        PlayerPanelManager.Coin =0;
        PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
        PlayerPrefs.SetInt("undoCount", ToolManager.undoCount);
        PlayerPrefs.SetInt("magnetCount", ToolManager.magnetCount);
        PlayerPrefs.SetInt("sortCount", ToolManager.sortCount);
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
