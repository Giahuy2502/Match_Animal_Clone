using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
    public void OnMagnetButton()
    {

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
