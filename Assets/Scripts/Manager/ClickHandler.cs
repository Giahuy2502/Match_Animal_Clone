﻿
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting;


public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    AudioSourceManager audioSourceManager => AudioSourceManager.Instance;
    public void OnPointerClick(PointerEventData eventData)
    {

        CellManager cell = GetComponent<CellManager>();
        
        if (DataGame.countTickedCell >= 7)
        {
            DataGame.stateCurrentPlay = 2;
            Debug.Log(DataGame.countTickedCell);
            Debug.Log("Lose game!");
            return;
        }
        if (cell.GetClickable())
        {
            Debug.Log("Continue game!");
            SetClickable();
            SoundEffect(4);
            DeleteInGrid(cell);
            MoveTickedCell();
        }
    }
    void SetClickable()
    {
        CellManager cell = GetComponent<CellManager>();
        cell.SetClickable(false);
    }
    void DeleteInGrid(CellManager cell)
    {
        int i = cell.GetI();
        int j = cell.GetJ();
        int layer = cell.GetLayer();
        DataGame.undoCell.Push(gameObject);
        GameObject[,] grid = DataGame.layerGrid[layer];
        grid[i, j] = null;
        Debug.Log("____***(da xoa tren gird)");
    }
   
    void MoveTickedCell()
    {
        for (int i = 0; i < DataGame.PositionTicked.Count; i++)
        {
            if (DataGame.listTickedCell[i] == null)
            {
                Vector3 To = DataGame.PositionTicked[i];
                DataGame.listTickedCell[i] = gameObject;
                StartCoroutine(ArrangeTickedCell());
                StartCoroutine(CheckDestroyOrLose());
                return;
            }
        }
    }
    void IncreaseArrIndex()
    {
        DataGame.countAllCell--;
        DataGame.countTickedCell++;
        int indexCell = GetIndexSprite(gameObject);
        DataGame.arrindex[indexCell]++;
        if (DataGame.arrindex[indexCell] == 3)
        {
            //DataGame.countTickedCell -= 3;
            //Debug.Log(DataGame.countTickedCell+"   da xoa 3 cell");
            TickedCellManager.checkDestroy = true;
        }
    }
    IEnumerator ArrangeTickedCell()
    {
        SortArrayObject();
        for (int i = 0; i < DataGame.listTickedCell.Length; i++)
        {
            if (DataGame.listTickedCell[i] == null || DataGame.listTickedCell[i].gameObject == null) break;
            Vector3 To = DataGame.PositionTicked[i];
            DataGame.listTickedCell[i].transform.DOMove(To, 0.25f);

        }
        yield return new WaitForSeconds(0.25f);
        
    }
    IEnumerator CheckDestroyOrLose()
    {
        yield return ArrangeTickedCell();
        IncreaseArrIndex();
        if (TickedCellManager.checkDestroy)
        {
            DestroyTickedCell(); // Hủy cell khi thỏa điều kiện
            SoundEffect(5);
            DataScore.state = 1;
            DataScore.combo++;
        }
        else if (DataGame.countTickedCell >= 7 && !TickedCellManager.checkDestroy)
        {
            DataGame.stateCurrentPlay = 2; // Chuyển sang trạng thái thua
            Debug.Log(DataGame.countTickedCell);
            Debug.Log("Lose game!");
            yield break;
        }

        if (DataGame.countAllCell == 0)
        {
            DataGame.stateCurrentPlay = 1; // Nếu không còn cell nào, chuyển trạng thái chơi tiếp
        }
        SortArrayAfterDestroy();
    }
    void DestroyTickedCell()
    {
        for (int i = 0; i < DataGame.listTickedCell.Length; i++)
        {
            if (DataGame.listTickedCell[i] != null)
            {
                GameObject obj = DataGame.listTickedCell[i];
                int indexCell = GetIndexSprite(obj);

                if (DataGame.arrindex[indexCell] >= 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int index = i + j;
                        GameObject objDes = DataGame.listTickedCell[index];
                        if (objDes != null)
                        {
                            DOTween.Kill(objDes);
                            Destroy(objDes);
                            DataGame.listTickedCell[index] = null;
                        }
                    }
                    DataGame.arrindex[indexCell] -= 3;

                    i = i + 2;
                }
            }
        }
        if (TickedCellManager.checkDestroy)
        {
            DataGame.countTickedCell -= 3;
            TickedCellManager.checkDestroy = false;
            
        }
    }

    static int GetIndexSprite(GameObject obj)
    {
        CellManager datacell = obj.GetComponent<CellManager>();
        int indexCell = datacell.GetIndexSprite();
        return indexCell;
    }

    static void SortArrayObject()
    {
        Array.Sort(DataGame.listTickedCell, (a, b) =>
        {
            if (a == null || b == null)
                return a == null ? 1 : -1;
            var aIndex = a.GetComponent<CellManager>();
            var bIndex = b.GetComponent<CellManager>();
            return aIndex.GetIndexSprite().CompareTo(bIndex.GetIndexSprite());
        });
    }

    static void SortArrayAfterDestroy()
    {
        SortArrayObject();
        for (int i = 0; i < DataGame.listTickedCell.Length; i++)
        {
            if (DataGame.listTickedCell[i] != null && DataGame.listTickedCell[i].gameObject != null)
            {
                Vector3 To = DataGame.PositionTicked[i];
                DataGame.listTickedCell[i].transform.DOMove(To, 0.25f); 
            }
        }
        if (DataGame.countTickedCell == 7)
        {
            DataGame.stateCurrentPlay = 2;
            Debug.Log(DataGame.countTickedCell);
            Debug.Log("Lose game!");
        }
    }
    public void SoundEffect(int index)
    {
        audioSourceManager.PlayAudio(index);
    }
}