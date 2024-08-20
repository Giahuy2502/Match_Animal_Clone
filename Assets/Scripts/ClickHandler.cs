using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHandler : MonoBehaviour,IPointerClickHandler
{
    int[] arrindex = new int[7];
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("click On: " + gameObject.name);
        //Debug.Log("Position: " + transform.position);
        TickedCell.listTickedCell.Add(gameObject);
        IncreaseArrIndex();
        MoveTickedCell();
        DestroyTickedCell();
    }

    void IncreaseArrIndex()
    {
        foreach (GameObject obj in TickedCell.listTickedCell)
        {
            CellManager datacell = obj.GetComponent<CellManager>();
            int indexCell = datacell.index;
            arrindex[indexCell]++;
        }
    }

    void DestroyTickedCell()
    {
        // Danh sách để lưu các phần tử cần xóa
        List<GameObject> toRemove = new List<GameObject>();

        // Duyệt qua danh sách chính
        foreach (GameObject obj in TickedCell.listTickedCell)
        {
            CellManager datacell = obj.GetComponent<CellManager>();
            int indexCell = datacell.index;

            // Nếu cần xóa
            if (arrindex[indexCell] == 3)
            {
                toRemove.Add(obj);
            }
        }

        // Xóa các phần tử sau khi duyệt xong
        foreach (GameObject obj in toRemove)
        {
            TickedCell.listTickedCell.Remove(obj);
            Destroy(obj);
        }
    }

    void MoveTickedCell()
    {
        if (TickedCell.listTickedCell.Count > 7)
        {
            //end game
        }
        else
        {
            int indexPositionTicked = TickedCell.listTickedCell.IndexOf(gameObject);
            //Debug.Log(indexPositionTicked);
            gameObject.transform.position = TickedCell.PositionTicked[indexPositionTicked];
        }
    }

}
