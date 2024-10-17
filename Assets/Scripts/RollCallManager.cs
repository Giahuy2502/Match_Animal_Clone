using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class RollCallManager : MonoBehaviour
{
    [SerializeField] string today;
    [SerializeField] string filePath;
    [SerializeField] AttendanceData data;
    [SerializeField] List<Image> images;
    [SerializeField] Button Button;

    private void Start()
    {

        today = DateTime.Now.Date.ToString("dd/MM/yyyy");
        PlayerPanelManager.Coin = PlayerPrefs.GetInt("coin", 0);
        Debug.Log(PlayerPanelManager.Coin);
        filePath = Application.persistentDataPath + "/attendance.json";
        LoadGame();
        InitUI(today);
        if(data.attendanceDates.Contains(today))
        {
            Button.interactable = false;
        }
    }
    public void OnCollectButton()
    {
        /*-neu list trong thi la chua co ngay nao diem danh:
         *   +danh dau hom nay la ngay dau tien
          
          - neu dang diem danh nma chua du 9 ngay:
             +cap nhat chi so ngay hom nay
             +them hom nay vao date
        */
        List<string> list = data.attendanceDates;
        int[] index = data.index;
        if (list.Count==0)
        {
            index[0] = 1;
            GetReward(ref PlayerPanelManager.Coin, 0);
            list.Add(today);
        }
        else
        {
            int currentDay = int.Parse(today.Substring(0,2));
            int firstDay = int.Parse(list[0].Substring(0,2));
            int indexBox = currentDay-firstDay;
            index[indexBox] = 1;
            GetReward(ref PlayerPanelManager.Coin, indexBox);
        }
        SaveGame();
        this.gameObject.SetActive(false);
    }
    public void InitUI(string today)
    {
        /* -neu ptu index nao =1 thi la ngay da diem danh
           -nguoc lai la chua diem danh
           -neu da diem danh 9 ngay:
             +xoa het list
             +reset lai mang index
        */
        List<string> list = data.attendanceDates;
        int[] index = data.index;
        if (list.Contains(today)) gameObject.SetActive(false);
        //Debug.Log($"first day = {list[0]} + list.count = {list.Count}");
        if (list.Count==0) return;
        int currentDay = int.Parse(today.Substring(0, 2));
        int firstDay = int.Parse(list[0].Substring(0, 2));
        int indexBox = currentDay - firstDay;
        if (indexBox > 9)
        {
            list.Clear();
            for (int i = 0; i < index.Length; i++)
            {
                data.index[i] = 0;
            }
        }
        for(int i = 0;i<index.Length;i++)
        {
            if (index[i]==1)
            {
                Image tmp= images[i];
                tmp.color = Color.green;
            }
        }

    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("coin",PlayerPanelManager.Coin); 
        string _data = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(filePath,_data);
        GameUtility.Log(this,"Da save game" + _data, Color.yellow);
    }
    public void LoadGame()
    {
        if (System.IO.File.Exists(filePath))
        {

            string _data = System.IO.File.ReadAllText(filePath);
            data =JsonUtility.FromJson<AttendanceData>(_data);
            GameUtility.Log(this,"Da load game"+_data, Color.yellow);
            Debug.Log(filePath);
        }

    }
    public void GetReward(ref int coin,int index)
    {
        switch (index)
        {
            case 0:
                coin += 100;
                break;
            case 1:
                coin += 150;
                break;
            case 2:
                coin += 200;
                break;
            case 3:
                coin += 250;
                break;
            case 4:
                coin += 300;
                break;
            case 5:
                coin += 350;
                break;
            case 6:
                coin += 400;
                break;
            case 7:
                coin += 450;
                break;
            case 8:
                coin += 500;
                break;

        }
    }
}
[System.Serializable]
public class AttendanceData
{
    public List<string> attendanceDates; // Danh sách các ngày điểm danh
    public int[] index= new int[10];
}
