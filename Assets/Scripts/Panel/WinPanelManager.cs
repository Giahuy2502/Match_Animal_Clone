using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WinPanelManager : MonoBehaviour
{
    [SerializeField] Image fillStar1;
    [SerializeField] Image fillStar2;
    [SerializeField] Image fillStar3;

    private void Start()
    {
        fillStar1.enabled = DataScore.star1;
        fillStar2.enabled = DataScore.star2;
        fillStar3.enabled = DataScore.star3;
    }
    public void OnContinueButton()
    {
        int nextlevel= BoardManager.levelCurrent+1;
        BoardManager.levelCurrent=nextlevel;
        Debug.Log(BoardManager.levelCurrent);
        PlayerPrefs.SetInt("level", nextlevel);
        SceneManager.LoadScene(1);
    }
    public void OnHomeButton()
    {
        SceneManager.LoadScene(0);
    }
}
