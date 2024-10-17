using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Services.Analytics.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceManager : MonoSingleton<ResourceManager>
{

    private int undoCount;
    private int magnetCount;
    private int sortCount;
    private int coin;
    public int GetUndoCount()
    {
        return undoCount;
    }
    public int GetMagnetCount()
    {
        return magnetCount;
    }
    public int GetSortCount()
    {
        return sortCount;
    }

    public int GetCoin()
    {
        return coin;
    }
    
   
    public void ResetUndoTool(int count=0)
    {
        undoCount = count;
    }
    public void ResetMagnetTool(int count = 0)
    {
        magnetCount = count;
    }
    public void ResetSortTool(int count = 0)
    {
        sortCount = count;
    }

    public void ResetCoin(int count = 0)
    {
        coin = count;
    }

    public void SetUndoTool(int quantity)
    {
        undoCount += quantity;
    }
    public void SetMagnetTool(int quantity) 
    {
        magnetCount += quantity;
    }
    public void SetSortTool(int quantity)
    {
        sortCount += quantity;
    }
    public void SetCoin(int Count)
    {
        coin += Count;
    }
    protected override void DoOnAwake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
        undoCount = PlayerPrefs.GetInt("undoCount", 0);
        magnetCount = PlayerPrefs.GetInt("magnetCount",0);
        sortCount= PlayerPrefs.GetInt("sortCount",0);
    }
    
   
   
    
}
