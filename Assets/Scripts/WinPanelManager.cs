using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanelManager : MonoBehaviour
{
    public void OnContinueButton()
    {
        SceneManager.LoadScene(0);
    }
    public void OnHomeButton()
    {

    }
}
