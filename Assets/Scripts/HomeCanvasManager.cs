using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeCanvasManager : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnResponButton()
    {

    }

    public void OnLanguageButton()
    {

    }
    public void OnShopButton()
    {
        SceneManager.LoadScene(2);
    }
    public void OnLevelsButton()
    {

    }
    public void OnCoinButton()
    {
        SceneManager.LoadScene(2);
    }
    public void OnGiftButton()
    {

    }
}
