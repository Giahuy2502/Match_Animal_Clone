using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WinPanelManager : MonoBehaviour
{
   
    [SerializeField] DataLevel dataLevel;
    [SerializeField] Image fillStar1;
    [SerializeField] Image fillStar2;
    [SerializeField] Image fillStar3;
    private int endLevel=20;
    private int nextlevel;
    UnlockLevel unlock => UnlockLevel.Instance;
    private void Start()
    {
        fillStar1.enabled = DataScore.star1;
        fillStar2.enabled = DataScore.star2;
        fillStar3.enabled = DataScore.star3;
        int starFill;
        if (DataScore.star3) starFill = 3;
        else if (DataScore.star2) starFill = 2;
        else if (DataScore.star1) starFill = 1;
        else starFill = 0;
        SaveStars(BoardManager.levelCurrent, starFill);
        SetUpNextLevel();
        UnLockNextLevel(nextlevel);
        Debug.Log(starFill);
    }

    private void SetUpNextLevel()
    {
        nextlevel = (BoardManager.levelCurrent + 1) % endLevel;
        BoardManager.levelCurrent = nextlevel;
    }

    public void OnContinueButton()
    {
        
        Debug.Log(BoardManager.levelCurrent);
        PlayerPrefs.SetInt("level", nextlevel);
        SceneManager.LoadScene("PlayScene");
    }
   
    public void SaveStars(int level, int stars)
    {
        PlayerPrefs.SetInt("Level_" + level + "_Stars", stars);
        PlayerPrefs.Save();
    }
    public void UnLockNextLevel(int nextLevel)
    {
        Debug.Log(nextLevel);
        unlock.unlockData.unlocked[nextLevel - 1] = true;

    }
}
