
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;


public class ClickHandler : MonoBehaviour,IPointerClickHandler
{

    bool checkDestroy = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        
        CellManager cell = GetComponent<CellManager>();
        if(cell.clickable)
        {
            // cell.clickable==false;
            SetClickable();
            DeleteInGrid(cell);
            CheckEndGame();
        }
    }
    void SetClickable()
    {
        CellManager cell= GetComponent<CellManager>();
        cell.clickable = false;
    }
    void DeleteInGrid(CellManager cell)
    {
        int i = cell.i;
        int j = cell.j;
        int layer =cell.layer;
        DataGame.undoCell.Push(gameObject);
        GameObject[,] grid = DataGame.layerGrid[layer];
        grid[i, j] = null;
    }
    void CheckEndGame()
    {
        if (DataGame.countTickedCell>7)
        {
            DataGame.stateCurrentPlay = 2;
            Debug.Log("Lose game!");
        }
        else
        {
            MoveTickedCell();
            DataGame.countAllCell--;
            DataGame.countTickedCell++;
            Debug.Log(DataGame.countTickedCell);
            //Debug.Log(DataGame.countAllCell);
        }
    }
    void MoveTickedCell()
    {
        for (int i = 0; i < DataGame.PositionTicked.Count; i++)
        {
            if (DataGame.listTickedCell[i]==null)
            {
                Vector3 To = DataGame.PositionTicked[i];
                DataGame.listTickedCell[i] = gameObject;
                
                transform.DOMove(To, 0.25f).OnComplete(() =>
                {
                    IncreaseArrIndex();
                    ArrangeTickedCell();
                });
                break;
            }
        }
    }
    void IncreaseArrIndex()
    {
        
        int indexCell = GetIndexSprite(gameObject);
        DataGame.arrindex[indexCell]++;
        if (DataGame.arrindex[indexCell] == 3)
        {
            DataGame.countTickedCell -= 3;
            Debug.Log(DataGame.countTickedCell+"   da xoa 3 cell");
            checkDestroy = true;
        }
    }
    void ArrangeTickedCell()
    {
        SortArrayObject();
        for (int i = 0; i < DataGame.listTickedCell.Length; i++)
        {
            if (DataGame.listTickedCell[i] == null) break;
            Vector3 To = DataGame.PositionTicked[i];
            DataGame.listTickedCell[i].transform.DOMove(To, 0.25f).OnComplete(() =>
            {
                if(checkDestroy)
                {  
                    DestroyTickedCell();
                }
                if (DataGame.countAllCell == 0) DataGame.stateCurrentPlay = 1;
            });
        }
        
    }
    void DestroyTickedCell()
    {
        for(int i = 0; i < 7; i++)
        {
            if (DataGame.listTickedCell[i] != null)
            {
                GameObject obj = DataGame.listTickedCell[i];
                int indexCell = GetIndexSprite(obj);
                if (DataGame.arrindex[indexCell] >= 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int index = i+j;
                        GameObject objDes = DataGame.listTickedCell[index];
                        DOTween.Kill(objDes);
                        Destroy(objDes);
                        DataGame.listTickedCell[index] = null;
                    }
                    DataGame.arrindex[indexCell] -= 3;
                    i = i + 2;
                }
            }
        }
        if(checkDestroy) 
        {
            
            checkDestroy = false;
            SortArrayAfterDestroy();
        }
    }

    static int GetIndexSprite(GameObject obj)
    {
        CellManager datacell = obj.GetComponent<CellManager>();
        int indexCell = datacell.indexSprite;
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
            return aIndex.indexSprite.CompareTo(bIndex.indexSprite);
        });
    }

    static void SortArrayAfterDestroy()
    {
        SortArrayObject();
        for (int i = 0; i < DataGame.listTickedCell.Length; i++)
        {
            if (DataGame.listTickedCell[i] != null)
            {
                Vector3 To = DataGame.PositionTicked[i];
                DataGame.listTickedCell[i].transform.DOMove(To, 0.25f);
            }           
        }
    }

}
