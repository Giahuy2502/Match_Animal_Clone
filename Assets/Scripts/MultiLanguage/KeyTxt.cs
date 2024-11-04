using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyTxt
{
    Spin_LuckySpin,
    Spin_FreeSpins,
    Home_Level,
    Home_Levels,
    Player_Combo,
    Pause_Continue,
    Pause_Restart,
    Pause_Home,
    Pause_Level,
    Pause_Tutorial,
    Win_DeleteLevel,
    Lose_Continue,
    Lose_Text,
    Lose_Free,
    Store_store,
    Gift_giftBox,
    Tutorial_HowtoPlay,
    Tutorial_Text,
    Home_Respon_Title,
    Home_Respon_Txt,
    Home_Respon_button
}
public static class ConvertEnumToString
{
    public static string GetStringByKey(KeyTxt key)
    {
        
        return key.ToString();
    }
}

//public class Client
//{
//    private int result;

//    public void SetResult()
//    {
//        result = apdater.getresult();
//    }
//}

//public static class apdater
//{
//    private static IAdapter handlingAdapter;

//    public static int getresult()
//    {
//        handlingAdapter = new enumadapter();
//        return handlingAdapter.GetValue();
//    }
//}

//public interface IAdapter
//{
//    int GetValue();
//}

//public class enumadapter : IAdapter
//{
//    public KeyTxt keyTxt;
//    public int GetValue()
//    {
//       return HandleGettingResult();
//    }

//    private int HandleGettingResult()
//    {
//        // Xu li chuyen tu KeyTxt thanh enum
//        return 0;
//    }
//}
