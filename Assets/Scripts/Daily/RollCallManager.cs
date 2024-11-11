using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class RollCallManager : MonoBehaviour
{
    [SerializeField] private string filePath;
    [SerializeField] private AttendanceData data;
    [SerializeField] private List<Image> images;
    [SerializeField] private Button Button;
    [SerializeField] private long tickNow;

    ResourceManager ResourceManager=> ResourceManager.Instance;
    private void Start()
    {
        tickNow = DateTime.Now.Date.Ticks;
        LoadData();
        InitUI(tickNow);
    }
    public void OnCollectButton()
    {
        /*-neu list trong thi la chua co ngay nao diem danh:
         *   +danh dau hom nay la ngay dau tien
          
          - neu dang diem danh nma chua du 9 ngay:
             +cap nhat chi so ngay hom nay
             +them hom nay vao date
        */
        List<long> list = data.attendanceDates;
        
        GetReward(list.Count);
        ClearList(list);
        list.Add(tickNow);
        SaveData();
        SpinManager.checkable = true;
        Exit();
    }   
    private static void ClearList(List<long> list)
    {
        if (list.Count == 9)
        {
            list.Clear();
        }
    }
    public void InitUI(long tickNow)
    {
        List<long> list = data.attendanceDates;
        if (list.Contains(tickNow)) Exit();       
        SetColorUI(list);
    }
    private void SetColorUI(List<long> list)
    {
        if (list.Count == 0) return;

        for (int i = 0; i < list.Count; i++)
        {
            images[i].color = Color.green;
        }

        for (int i = list.Count; i < images.Count; i++)
        {
            images[i].color = Color.white;
        }
    }
    private void Exit()
    {
        this.gameObject.SetActive(false);
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("coin",ResourceManager.GetCoin()); 
        string _data = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(filePath,_data);
        GameUtility.Log(this,"Da save game" + _data, Color.yellow);
    }
    public void LoadData()
    {
        filePath = Application.persistentDataPath + "/attendance.json";
        if (System.IO.File.Exists(filePath))
        {
            string _data = System.IO.File.ReadAllText(filePath);
            data =JsonUtility.FromJson<AttendanceData>(_data);
        }

    }
    public void GetReward(int index)
    {
        switch (index)
        {
            case 0:
                ResourceManager.SetCoin(100);
                break;
            case 1:
                ResourceManager.SetCoin(150);
                break;
            case 2:
                ResourceManager.SetCoin(200);
                break;
            case 3:
                ResourceManager.SetCoin(250);
                break;
            case 4:
                ResourceManager.SetCoin(300);
                break;
            case 5:
                ResourceManager.SetCoin(350);
                break;
            case 6:
                ResourceManager.SetCoin(400);
                break;
            case 7:
                ResourceManager.SetCoin(450);
                break;
            case 8:
                ResourceManager.SetCoin(500);
                break;
        }
        SpinManager.checkable = true;
    }
}
[System.Serializable]
public class AttendanceData
{
    public List<long> attendanceDates; // Danh sách các ngày điểm danh
    
}
