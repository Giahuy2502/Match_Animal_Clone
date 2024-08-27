using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour
{
    public void OnContinueButton()
    {
        this.gameObject.SetActive(false);
    }
    public void OnRestartButton()
    {
        SceneManager.LoadScene(0);
    }
    public void OnHomeButton()
    {

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
