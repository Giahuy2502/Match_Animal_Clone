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
    [SerializeField] private UserData userData;
    [SerializeField] private string jsonDefault;
    [SerializeField] List<GiftType> enumItem;
    protected override void DoOnAwake()
    {
        string json = UserData.GetJsonData(jsonDefault);
        userData = new UserData(json);
    }
    public void SetTypeItem(GiftType type,int count)
    {
        int n=0;
        
        for(int i=0; i<enumItem.Count;i++)
        {
            if (enumItem[i].Equals(type))
            {
                n=i;
                break;
            }
        }
        if (n==0) SetUndoTool(count);
        else if(n==1) SetMagnetTool(count);
        else if (n==2) SetSortTool(count);
        else SetCoin(count);
    }


    public int GetUndoCount()
    {
        return userData.undoCount;
    }
    public int GetMagnetCount()
    {
        return userData.magnetCount;
    }
    public int GetSortCount()
    {
        return userData.sortCount;
    }
    public int GetCoin()
    {
        return userData.coin;
    }
    
    public void ResetUndoTool(int count=0)
    {
        userData.undoCount = count;
    }
    public void ResetMagnetTool(int count = 0)
    {
        userData.magnetCount = count;
    }
    public void ResetSortTool(int count = 0)
    {
        userData.sortCount = count;
    }
    public void ResetCoin(int count = 0)
    {
        userData.coin = count;
    }


    public void SetUndoTool(int quantity)
    {
        userData.undoCount += quantity;
    }
    public void SetMagnetTool(int quantity) 
    {
        userData.magnetCount += quantity;
    }
    public void SetSortTool(int quantity)
    {
        userData.sortCount += quantity;
    }
    public void SetCoin(int Count)
    {
        userData.coin += Count;
    }
    
    
    public void SetProduct(int coin,int undo,int magnet,int sort)
    {
        SetUndoTool(undo);
        SetMagnetTool(magnet);
        SetSortTool(sort);
        SetCoin(coin);
        userData.SaveData();
    }
    public void OnApplicationQuit()
    {
        userData.SaveData();

    }
 
}
