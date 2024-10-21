using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickedCellManager : MonoBehaviour
{
    public List<Vector3> PositionTicked = new List<Vector3>();
    public Queue<GameObject> Queue = new Queue<GameObject>();
    public static bool checkDestroy;

    void Awake()
    {
        foreach(RectTransform child in GetComponentsInChildren<RectTransform>())
        {
            if (child != transform)
            {
                Vector3 pos = child.position;
                PositionTicked.Add(pos);
            }
        }
        DataGame.PositionTicked = PositionTicked;

    }
  
    
}

