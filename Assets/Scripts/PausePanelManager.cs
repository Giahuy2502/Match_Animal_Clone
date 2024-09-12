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
