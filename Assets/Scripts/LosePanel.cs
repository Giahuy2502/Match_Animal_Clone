using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    int indexScene;
    private void Start()
    {
        indexScene = SceneManager.GetActiveScene().buildIndex;
    }
    public void OnHomeButton()
    {
        SceneManager.LoadScene(0);
    }
    public void OnRestartButton()
    {
        SceneManager.LoadScene(indexScene);
    }
    public void OnFreeButton()
    {

    }
    public void OnBuyButton()
    {

    }
}
