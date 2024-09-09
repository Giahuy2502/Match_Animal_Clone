using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolManager : MonoBehaviour
{
    public void OnUndoButton()
    {
        GameObject undoCell = DataGame.undoCell.Pop();
        if (undoCell == null) return;
        CellManager cell = undoCell.GetComponent<CellManager>();
        int i = cell.i;
        int j = cell.j;
        int layer = cell.layer;
        GameObject[,] grid = DataGame.layerGrid[layer];
        grid[i, j] = undoCell;
        for (int z = 0; z < DataGame.PositionTicked.Count; z++)
        {
            if (DataGame.listTickedCell[z] == undoCell)
            {
                DataGame.listTickedCell[z] = null;
                Debug.Log("da xoa khoi list tickedcell");
            }
        }
        SortArrayObject();
        for (int z = 0; z < DataGame.listTickedCell.Length; z++)
        {
            if (DataGame.listTickedCell[z] != null)
            {
                Vector3 To = DataGame.PositionTicked[z];
                DataGame.listTickedCell[z].transform.DOMove(To, 0.25f);
            }
        }
        DataGame.countAllCell++;
        DataGame.countTickedCell--;
        int indexCell = cell.indexSprite;
        DataGame.arrindex[indexCell]--;
        undoCell.transform.DOMove(cell.undoPosition, 0.25f);
        cell.clickable = true;
    }
    public void OnMagnetButton()
    {
        // duyet list ticked cell
        // chon ra cell co so dem lon nhat
        // chon ra cac cell cung loai voi cell tren tu gird
        // set cell.clickable cua cac cell duoc chon la true
        int count = 0;
        int indexSprite=0;
        for(int i=0;i<DataGame.listTickedCell.Length;i++)
        {
            if (DataGame.listTickedCell[i] == null) break;
            CellManager cell= DataGame.listTickedCell[i].GetComponent<CellManager>();
            if (DataGame.arrindex[cell.indexSprite] > count)
            {
                indexSprite = cell.indexSprite;
                count = DataGame.arrindex[indexSprite];
            }
        }
        Debug.Log($"count : {count} + indexSprite : {indexSprite}");
        List<GameObject[,]> board = DataGame.layerGrid;
        for(int i= board.Count-1;i>=0;i--)
        {
            GameObject[,] boardCell = board[i];
            for(int j=0;j<boardCell.GetLength(0);j++)
            {
                for(int k=0;k<boardCell.GetLength(1);k++)
                {
                    if (boardCell[j,k]!=null)
                    {

                        CellManager cell = boardCell[j,k].GetComponent<CellManager>();
                        cell.clickable = true;
                        Image image = cell.GetComponent<Image>();
                        image.color = Color.white;
                        if (cell.indexSprite == indexSprite&&count<3)
                        {
                            ClickHandler clickCell = boardCell[j,k].GetComponent<ClickHandler>();
                            PointerEventData eventData = new PointerEventData(EventSystem.current);
                            clickCell.OnPointerClick(eventData);
                            count++;
                        }
                        if(count==3) return;
                    }
                }
            }
        }

    }
    public void OnSortingButton()
    {

    }
    static void SortArrayObject()
    {
        Array.Sort(DataGame.listTickedCell, (a, b) =>
        {
            if (a == null || b == null)
                return a == null ? 1 : -1;
            var aIndex = a.GetComponent<CellManager>();
            var bIndex = b.GetComponent<CellManager>();
            return aIndex.indexSprite.CompareTo(bIndex.indexSprite);
        });
    }
}
