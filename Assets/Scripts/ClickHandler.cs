
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class ClickHandler : MonoBehaviour,IPointerClickHandler
{
    int count = 0;
    bool checkDestroy = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        CheckEndGame();
    }
    void CheckEndGame()
    {
        if (count > 7)
        {
            Debug.Log("Lose game!");
        }
        else
        {
            MoveTickedCell();
            TickedCell.countAllCell--;
            Debug.Log(TickedCell.countAllCell);
        }
    }
    void MoveTickedCell()
    {
        for (int i = 0; i < TickedCell.PositionTicked.Count; i++)
        {
            if (TickedCell.listTickedCell[i]==null)
            {
                Vector3 To = TickedCell.PositionTicked[i];
                TickedCell.listTickedCell[i] = gameObject;
                count++;
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
        TickedCell.arrindex[indexCell]++;
        if (TickedCell.arrindex[indexCell] == 3) checkDestroy = true;
    }
    void ArrangeTickedCell()
    {
        SortArrayObject();
        for (int i = 0; i < TickedCell.listTickedCell.Length; i++)
        {
            if (TickedCell.listTickedCell[i] == null) break;
            Vector3 To = TickedCell.PositionTicked[i];
            TickedCell.listTickedCell[i].transform.DOMove(To, 0.25f).OnComplete(() =>
            {
                if(checkDestroy) DestroyTickedCell();
                if (TickedCell.countAllCell == 0) SceneManager.LoadScene(0);
            });
        }
        
    }
    void DestroyTickedCell()
    {
        for(int i = 0; i < 7; i++)
        {
            if (TickedCell.listTickedCell[i] != null)
            {
                GameObject obj = TickedCell.listTickedCell[i];
                int indexCell = GetIndexSprite(obj);
                if (TickedCell.arrindex[indexCell] >= 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int index = i+j;
                        GameObject objDes = TickedCell.listTickedCell[index];
                        DOTween.Kill(objDes);
                        Destroy(objDes);
                        TickedCell.listTickedCell[index] = null;
                    }
                    TickedCell.arrindex[indexCell] -= 3;
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
        Array.Sort(TickedCell.listTickedCell, (a, b) =>
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
        for (int i = 0; i < TickedCell.listTickedCell.Length; i++)
        {
            if (TickedCell.listTickedCell[i] != null)
            {
                Vector3 To = TickedCell.PositionTicked[i];
                TickedCell.listTickedCell[i].transform.DOMove(To, 0.25f);
            }           
        }
    }

}
