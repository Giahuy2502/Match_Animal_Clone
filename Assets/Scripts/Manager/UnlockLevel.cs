using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevel : MonoSingleton<UnlockLevel>
{
    [SerializeField] public  UnlockData unlockData;
    [SerializeField] string jsonDefault;
    protected override void DoOnAwake()
    {
        string json = UnlockData.GetJsonData(jsonDefault);
        Debug.Log(json);
        unlockData = new UnlockData(json);
    }
    public void SaveData()
    {
        unlockData.SaveData();
    }
}
