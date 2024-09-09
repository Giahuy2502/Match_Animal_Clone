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
        //dua cac layerGrid ve list cac gameobject
        //tron danh sach 1 chieu bang thuat toan fisher-yates
        //thay doi cac chi so i,j,layer cua cellManager
        //gan lai gia tri tu danh sach tron ve lai cac layer grid
        List<GameObject[,]> board = DataGame.layerGrid;
        List<int> tempList = new List<int>();
        foreach(var Grid in board)
        {
            for(int i = 0; i < Grid.GetLength(0); i++)
            {
                for(int j=0;j<Grid.GetLength(1); j++)
                {
                    if (Grid[i, j] != null)
                    {
                        
                        CellManager cell = Grid[i, j].GetComponent<CellManager>();
                        tempList.Add(cell.indexSprite);
                        // tempList[tempList.Count-1]
                        // Debug.Log($"cell.layer= {cell.layer} +cell.i= {cell.i}+cell.j= {cell.j}");
                    }

                }
            }
        }
        for (int i = tempList.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            int temp = tempList[i];
            tempList[i] = tempList[randomIndex];
            tempList[randomIndex] = temp;
        }
        int index = 0;
        for(int k=0;k<DataGame.layerGrid.Count;k++)
        {
            GameObject[,] Grid = DataGame.layerGrid[k];
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Grid[i, j] != null)
                    {
                        
                        CellManager tempCell = Grid[i,j].GetComponent<CellManager>();
                        tempCell.indexSprite = tempList[index];
                        tempCell.SetUpSprite(tempCell.indexSprite);
                        index++;
                    }

                }
            }
        }
        
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
