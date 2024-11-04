using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int maxScore;
    [SerializeField] int currentScore;
    [SerializeField] Level dataLevel;
    [SerializeField] Image fillScore;
    [SerializeField] Image fillCombo;
    [SerializeField] Image fillStar1;
    [SerializeField] Image fillStar2;
    [SerializeField] Image fillStar3;
    float countTime = 4f;
    float fillStar = 0.135f;
    float startFill1 = 0.15f;
    float startFill2 = 0.44f;
    float startFill3 = 0.725f;
    private void Start()
    {
        currentScore = 0;
        fillStar1.fillAmount = 0;
        fillStar2.fillAmount = 0;
        fillStar3.fillAmount = 0;
        DataScore.star1 = false;
        DataScore.star2 = false;
        DataScore.star3 = false;
    }
    public void setMaxScore(int maxScore)
    {
        this.maxScore = maxScore;
    }
    private void Update()
    {
        //nếu đánh dấu là đã ăn thì sẽ cộng điểm theo công thức
        //khi ăn thì reset count time về lại 2s
        //sau 1 khoảng thời gian nhất định mà ko ăn thì sẽ bị mất combo
        // sau khi đến 1 mức nhất định sẽ lấp đầy các ngôi sao
        if(DataScore.state==1)
        {
            currentScore = currentScore + 10 * DataScore.combo;
            DataScore.state = 0;
            DataScore.countTime = countTime;
            //Debug.Log($"countTime = {DataScore.countTime}+ combo = {DataScore.combo}+1");
            float percentCoin = (float)currentScore / maxScore;
            fillScore.fillAmount = percentCoin;
            FillAllStar(percentCoin);
        }
        CountTimeForCombo();
        
    }

    private void FillAllStar(float percentCoin)
    {
        if (percentCoin < startFill2 && percentCoin > startFill1)
        {
            float starFill1 = (percentCoin - startFill1) / fillStar;
            fillStar1.fillAmount = starFill1;
            fillStar3.fillAmount = fillStar2.fillAmount = 0f;
            if (percentCoin >= startFill1 + fillStar) DataScore.star1 = true;
        }
        else if (percentCoin < startFill3 && percentCoin > startFill2)
        {
            float starFill2 = (percentCoin - startFill2) / fillStar;
            fillStar2.fillAmount = starFill2;
            fillStar1.fillAmount = 1f;
            fillStar3.fillAmount = 0f;
            if (percentCoin >= startFill2 + fillStar) DataScore.star2 = true;
        }
        else if (percentCoin > startFill3)
        {
            float starFill3 = (percentCoin - startFill3) / fillStar;
            fillStar3.fillAmount = starFill3;
            fillStar1.fillAmount = fillStar2.fillAmount = 1f;
            if (percentCoin >= startFill3 + fillStar) DataScore.star3 = true;
        }
    }

    void CountTimeForCombo()
    {
        if(DataScore.countTime > 0)
        {
            DataScore.countTime-=Time.deltaTime;
        }
        else
        {
            DataScore.combo = 0;
            DataScore.countTime = 0;
        }
        float percentCombo = DataScore.countTime / countTime;
        fillCombo.fillAmount = percentCombo;
    }
}
public static class DataScore
{
    public static int state = 0;
    public static int combo = 0;
    public static float countTime = 0f;
    public static bool star1;
    public static bool star2;
    public static bool star3;
}