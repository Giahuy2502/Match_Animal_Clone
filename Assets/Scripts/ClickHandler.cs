
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using System.Collections;


public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {

        CellManager cell = GetComponent<CellManager>();
        //Debug.Log($"indexSptite ={cell.indexSprite}");
        if (DataGame.countTickedCell >= 7)
        {
            DataGame.stateCurrentPlay = 2;
            Debug.Log(DataGame.countTickedCell);
            Debug.Log("Lose game!");
            return;
        }
        if (cell.clickable)
        {
            // cell.clickable==false;
            SetClickable();
            DeleteInGrid(cell);
            CheckEndGame();
        }
    }
    void SetClickable()
    {
        CellManager cell = GetComponent<CellManager>();
        cell.clickable = false;
    }
    void DeleteInGrid(CellManager cell)
    {
        int i = cell.i;
        int j = cell.j;
        int layer = cell.layer;
        DataGame.undoCell.Push(gameObject);
        GameObject[,] grid = DataGame.layerGrid[layer];
        grid[i, j] = null;
    }
    void CheckEndGame()
    {
        //cập nhật các chỉ số đếm
        //thực hiện di chuyển và sắp xếp các cell
        //xét điều kiện end game

        if (DataGame.countTickedCell >= 7)
        {
            DataGame.stateCurrentPlay = 2;
            Debug.Log(DataGame.countTickedCell);
            Debug.Log("Lose game!");
            return;
        }
        else
        {
            DataGame.countAllCell--;
            DataGame.countTickedCell++;
            Debug.Log($"countTickedCell = {DataGame.countTickedCell}");
            
            MoveTickedCell();
        }
    }
    void MoveTickedCell()
    {
        for (int i = 0; i < DataGame.PositionTicked.Count; i++)
        {
            if (DataGame.listTickedCell[i] == null)
            {
                Vector3 To = DataGame.PositionTicked[i];
                DataGame.listTickedCell[i] = gameObject;
                IncreaseArrIndex();
                StartCoroutine(ArrangeTickedCell());
                return;
            }
        }
    }
    void IncreaseArrIndex()
    {
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
        CheckDestroyOrLose();
    }
    void CheckDestroyOrLose()
    {
        if (TickedCellManager.checkDestroy)
        {
            DestroyTickedCell(); // Hủy cell khi thỏa điều kiện
            DataScore.state = 1;
            DataScore.combo++;
        }
        else if (DataGame.countTickedCell >= 7 && !TickedCellManager.checkDestroy)
        {
            DataGame.stateCurrentPlay = 2; // Chuyển sang trạng thái thua
            Debug.Log(DataGame.countTickedCell);
            Debug.Log("Lose game!");
            return;
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

}