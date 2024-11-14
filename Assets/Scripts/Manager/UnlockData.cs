using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UnlockData
{
    public  List<bool> unlocked = new List<bool>();
    private const string unlockKey = "unlockKey";
    public UnlockData(string json)
    {
        var unlockData = JsonUtility.FromJson<UnlockData>(json);
        unlocked = unlockData.unlocked;    
    }
    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
    public static string GetJsonData(string jsonDefault = null)
    {
        var jsonData = PlayerPrefs.GetString(unlockKey, jsonDefault);
        return jsonData;
    }
    public void SaveData()
    {
        PlayerPrefs.SetString(unlockKey, ToString());
        PlayerPrefs.Save();
    }
   
}
