
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ClickHandler : MonoBehaviour,IPointerClickHandler
{
    int count = 0;
    int countDestroy=-1;
    public void OnPointerClick(PointerEventData eventData)
    {
        
        CheckEndGame();
        
    }

    void IncreaseArrIndex()
    {
        CellManager datacell = gameObject.GetComponent<CellManager>();
        int indexCell = datacell.indexSprite;
        TickedCell.arrindex[indexCell]++;
    }

    void DestroyTickedCell()
    {
        for(int i = 0; i < 7; i++)
        {
            if (TickedCell.listTickedCell[i] != null)
            {
                GameObject obj = TickedCell.listTickedCell[i];
                CellManager datacell = obj.GetComponent<CellManager>();
                int indexCell = datacell.indexSprite;
                if (TickedCell.arrindex[indexCell] == 3)
                {
                    countDestroy = indexCell;
                    
                    DOTween.Kill(obj);
                    Destroy(obj);
                    Debug.Log("da xoa cell tai o : " + i);
                    //obj.SetActive(false);
                    //TickedCell.listTickedCell[i] = null;
                    TickedCell.ticked[i] = false;
                }
            }
        }
        if(countDestroy != -1) 
        {
            TickedCell.arrindex[countDestroy] = 0;
            //Debug.Log(countDestroy+" " + TickedCell.arrindex[countDestroy]+" _003");
            countDestroy = -1;
        }  
    }

    void MoveTickedCell()
    {
        for (int i = 0; i < TickedCell.PositionTicked.Count; i++)
        {
            if (!TickedCell.ticked[i])
            {
                Vector3 To = TickedCell.PositionTicked[i];

                transform.DOMove(To, 0.25f).OnComplete(() =>
                {
                    IncreaseArrIndex();
                    //Invoke("DestroyTickedCell", 0.5f);
                    DestroyTickedCell();
                });
                TickedCell.ticked[i] = true;
                TickedCell.listTickedCell[i] = gameObject;
                count++;
                break;
            }
        }
    }
    void CheckEndGame()
    {
        if (count > 7)
        {
            //lose game
        }
        else
        {
            MoveTickedCell();
            TickedCell.countAllCell--;
            Debug.Log(TickedCell.countAllCell);
            if (TickedCell.countAllCell == 0) SceneManager.LoadScene(0);
        }
    }
}
