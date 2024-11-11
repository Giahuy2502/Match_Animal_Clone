using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData 
{
    public int undoCount;
    public int magnetCount;
    public int sortCount;
    public int coin; 

    private const string userKey = "userKey";
    public UserData(string json)
    {
        var userData = JsonUtility.FromJson<UserData>(json);
        
        undoCount = userData.undoCount;
        magnetCount = userData.magnetCount;
        sortCount = userData.sortCount;
        coin = userData.coin;

    }
    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
    public static string GetJsonData(string jsonDefault = null)
    {
        var jsonData=PlayerPrefs.GetString(userKey, jsonDefault);
        return jsonData;
    }
    public void SaveData()
    {
        PlayerPrefs.SetString(userKey,ToString());
        PlayerPrefs.Save();
    }
}
