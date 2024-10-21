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

    ResourceManager ResourceManager=> ResourceManager.Instance;
    private void Start()
    {

        today = DateTime.Now.Date.ToString("dd/MM/yyyy");
        var tickNow = DateTime.Now.Date.Ticks;
        ResourceManager.SetCoin(PlayerPrefs.GetInt("coin", 0)) ;
        Debug.Log(ResourceManager.GetCoin());
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
            GetReward(0);
            list.Add(today);
        }
        else
        {
            int currentDay = int.Parse(today.Substring(0,2));
            int firstDay = int.Parse(list[0].Substring(0,2));
            int indexBox = currentDay-firstDay;
            index[indexBox] = 1;
            GetReward(indexBox);
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
        PlayerPrefs.SetInt("coin",ResourceManager.GetCoin()); 
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
    }
}
[System.Serializable]
public class AttendanceData
{
    public List<string> attendanceDates; // Danh sách các ngày điểm danh
    public int[] index= new int[10];
}
