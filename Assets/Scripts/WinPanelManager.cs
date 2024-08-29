using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanelManager : MonoBehaviour
{
    int indexCurrentScene;
    public void OnContinueButton()
    {
        indexCurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexCurrentScene+1);
    }
    public void OnHomeButton()
    {

    }
}
