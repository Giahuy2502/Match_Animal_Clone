using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GiftReward
{
    [SerializeField] private int count;
    [SerializeField] private GiftType type;

    public GiftType GetGiftType()
    {
        return type;
    }
    public int GetCount()
    {
        return count;
    }
}
