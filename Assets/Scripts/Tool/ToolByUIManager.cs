using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolByUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI undoTxt;
    [SerializeField] TextMeshProUGUI magTxt;
    [SerializeField] TextMeshProUGUI sortTxt;
    [SerializeField] GameObject giftPanel;
    [SerializeField] Image GiftBG;
    [SerializeField] float speedRotation = 15f;

    private int index=6;
    
    ResourceManager ResourceManager => ResourceManager.Instance;
    AudioSourceManager audioSourceManager => AudioSourceManager.Instance;

    void SetIndex(int count)
    {
        index = count;
    }
    private void Update()
    {
        GiftBG.transform.Rotate(0f, 0f, speedRotation * Time.deltaTime);
        undoTxt.text = ResourceManager.GetUndoCount().ToString();
        magTxt.text = ResourceManager.GetMagnetCount().ToString();
        sortTxt.text = ResourceManager.GetSortCount().ToString();

    }

    public void OnUndoButton()
    {
        SetIndex(6);
        if (ResourceManager.GetUndoCount() <= 0 && ResourceManager.GetCoin() < 300)
        {
            SetIndex(2);
            return;
        }
        else if (ResourceManager.GetUndoCount() <= 0 && ResourceManager.GetCoin() >= 300)
        {
            SetIndex(1);
            if (DataGame.undoCell.Count == 0) return;
            ResourceManager.SetCoin(-300);
            PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
            GameUtility.Log(this, $"ResourceManager.GetCoin() = {ResourceManager.GetCoin()}", Color.cyan);
            ResourceManager.SetUndoTool(1);
            
        }
        bool checkUndoable;
        GameObject undoCell;
        while (true)
        {
            if (DataGame.undoCell.Count == 0) return;
            undoCell = DataGame.undoCell.Pop();
            if (undoCell != null)
            {
                checkUndoable = true;
                break;
            }
            else
            {
                Destroy(undoCell);
            }
        }
        if (!checkUndoable) return;
        CellManager cell;
        int i, j, layer;
        GetIndexPositionCell(undoCell, out cell, out i, out j, out layer);
        GameObject[,] grid = DataGame.layerGrid[layer];
        grid[i, j] = undoCell;
        DeleteCellInList(undoCell); // xoa cell khoi list tickedcell
        SortArrayObject();
        SortListTickedCell();
        ResetAllCountNumber(cell);
        undoCell.transform.DOMove(cell.GetUndoPosition(), 0.25f);
        cell.SetClickable(true);
        //----
        ResourceManager.SetUndoTool(-1);
        PlayerPrefs.SetInt("undoCount", ResourceManager.GetUndoCount());
    }

    private static void ResetAllCountNumber(CellManager cell)
    {
        DataGame.countAllCell++;
        DataGame.countTickedCell--;
        int indexCell = cell.GetIndexSprite();
        DataGame.arrindex[indexCell]--;
    }
    private static void SortListTickedCell()
    {
        for (int z = 0; z < DataGame.listTickedCell.Length; z++)
        {
            if (DataGame.listTickedCell[z] != null)
            {
                Vector3 To = DataGame.PositionTicked[z];
                DataGame.listTickedCell[z].transform.DOMove(To, 0.25f);
            }
        }
    }
    private static void DeleteCellInList(GameObject undoCell)
    {
        for (int z = 0; z < DataGame.PositionTicked.Count; z++)
        {
            if (DataGame.listTickedCell[z] == undoCell)
            {
                DataGame.listTickedCell[z] = null;
                Debug.Log("da xoa khoi list tickedcell");
            }
        }
    }
    private static void GetIndexPositionCell(GameObject undoCell, out CellManager cell, out int i, out int j, out int layer)
    {
        cell = undoCell.GetComponent<CellManager>();
        i = cell.GetI();
        j = cell.GetJ();
        layer = cell.GetLayer();
    }

    //-----------------------------------------------------------------------------------------------------------
    public void OnMagnetButton()
    {
        // duyet list ticked cell
        // chon ra cell co so dem lon nhat
        // chon ra cac cell cung loai voi cell tren tu gird
        // set cell.clickable cua cac cell duoc chon la true
        SetIndex(6);
        if (ResourceManager.GetMagnetCount() <= 0 && ResourceManager.GetCoin() < 300)
        {
            SetIndex(2);
            return;
        }
        else if (ResourceManager.GetMagnetCount() <= 0 && ResourceManager.GetCoin() >= 300)
        {
            ResourceManager.SetCoin(-300);
            PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
            //GameUtility.Log(this, $"ResourceManager.GetCoin() = {ResourceManager.GetCoin()}", Color.cyan);
            ResourceManager.SetMagnetTool(1);
            SetIndex(1);
        }
        int count = 0;
        int indexSprite = 0;
        GetCountAndIndexSprite(ref count, ref indexSprite);
        Debug.Log($"count : {count} + indexSprite : {indexSprite}");
        GetSameCell(ref count, ref indexSprite);
        ResourceManager.SetMagnetTool(-1);
    }
    private void GetSameCell(ref int count, ref int indexSprite)
    {
        List<GameObject[,]> board = DataGame.layerGrid;
        for (int i = board.Count - 1; i >= 0; i--)
        {
            GameObject[,] boardCell = board[i];
            for (int j = 0; j < boardCell.GetLength(0); j++)
            {
                for (int k = 0; k < boardCell.GetLength(1); k++)
                {
                    if (boardCell[j, k] != null)
                    {

                        CellManager cell = boardCell[j, k].GetComponent<CellManager>();
                        cell.SetClickable(true);
                        cell.SetUpColor(Color.white);
                        if (count == 0 && indexSprite == 0) indexSprite = cell.GetIndexSprite();

                        if (cell.GetIndexSprite() == indexSprite && count < 3 && DataGame.countTickedCell <= 7)
                        {
                            ClickHandler clickCell = boardCell[j, k].GetComponent<ClickHandler>();
                            PointerEventData eventData = new PointerEventData(EventSystem.current);
                            clickCell.OnPointerClick(eventData);
                            count++;

                            
                        }
                        if (count == 3) return;
                    }
                }
            }
        }
    }


    private static void GetCountAndIndexSprite(ref int count, ref int indexSprite)
    {
        for (int i = 0; i < DataGame.listTickedCell.Length; i++)
        {
            if (DataGame.listTickedCell[i] == null) break;
            CellManager cell = DataGame.listTickedCell[i].GetComponent<CellManager>();
            if (DataGame.arrindex[cell.GetIndexSprite()] > count)
            {
                indexSprite = cell.GetIndexSprite();
                count = DataGame.arrindex[indexSprite];
            }
        }
    }


    //-----------------------------------------------------------------------------------------------------------
    public void OnSortingButton()
    {
        //dua cac layerGrid ve list cac gameobject
        //tron danh sach 1 chieu bang thuat toan fisher-yates
        //thay doi cac chi so i,j,layer cua cellManager
        //gan lai gia tri tu danh sach tron ve lai cac layer grid
        SetIndex(6);
        if (ResourceManager.GetSortCount() <= 0 && ResourceManager.GetCoin() < 300)
        {
            SetIndex(2);
            return;
        }
        else if (ResourceManager.GetSortCount() <= 0 && ResourceManager.GetCoin() >= 300)
        {
            ResourceManager.SetCoin(-300);
            PlayerPrefs.SetInt("coin", ResourceManager.GetCoin());
            GameUtility.Log(this, $"ResourceManager.GetCoin() = {ResourceManager.GetCoin()}", Color.cyan);
            ResourceManager.SetSortTool(1);
            SetIndex(1);
        }
        List<GameObject[,]> board = DataGame.layerGrid;
        List<int> tempList = new List<int>();
        ConvertBoardToListIndex(board, tempList);
        RandomSortList(tempList);
        ConvertListIndexToBoard(tempList);
        ResourceManager.SetSortTool(-1);
        PlayerPrefs.SetInt("sortCount", ResourceManager.GetSortCount());
    }

    private static void ConvertListIndexToBoard(List<int> tempList)
    {
        int index = 0;
        for (int k = 0; k < DataGame.layerGrid.Count; k++)
        {
            GameObject[,] Grid = DataGame.layerGrid[k];
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Grid[i, j] != null)
                    {

                        CellManager tempCell = Grid[i, j].GetComponent<CellManager>();
                        tempCell.SetIndexSprite(tempList[index]);
                        tempCell.SetUpSprite(tempCell.GetIndexSprite());
                        index++;
                    }

                }
            }
        }
    }
    private static void RandomSortList(List<int> tempList)
    {
        for (int i = tempList.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            int temp = tempList[i];
            tempList[i] = tempList[randomIndex];
            tempList[randomIndex] = temp;
        }
    }
    private static void ConvertBoardToListIndex(List<GameObject[,]> board, List<int> tempList)
    {
        foreach (var Grid in board)
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Grid[i, j] != null)
                    {

                        CellManager cell = Grid[i, j].GetComponent<CellManager>();
                        tempList.Add(cell.GetIndexSprite());
                        // tempList[tempList.Count-1]
                        // Debug.Log($"cell.layer= {cell.layer} +cell.i= {cell.i}+cell.j= {cell.j}");
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
            return aIndex.GetIndexSprite().CompareTo(bIndex.GetIndexSprite());
        });
    }
    //-----------------------------------------------------------------------------------------------------------
    public void OnGiftButton()
    {
        giftPanel.SetActive(true);

    }
    public void PlayEffect()
    {
        audioSourceManager.PlayAudio(index);
    }

}
