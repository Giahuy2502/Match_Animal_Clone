using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Services.Analytics.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DatabaseManager;

public class ToolManager : MonoBehaviour
{
    [SerializeField] GameObject giftPanel;
    [SerializeField] Image GiftBG;
    [SerializeField] float speedRotation = 15f;
    [SerializeField] TextMeshProUGUI undoTxt;
    [SerializeField] TextMeshProUGUI magTxt;
    [SerializeField] TextMeshProUGUI sortTxt;
    
    public static int undoCount;
    public static int magnetCount;
    public static int sortCount;

    private void Awake()
    {
        undoCount = PlayerPrefs.GetInt("undoCount", 0);
        magnetCount= PlayerPrefs.GetInt("magnetCount",0);
        sortCount= PlayerPrefs.GetInt("sortCount",0);
    }
    private void OnEnable()
    {
        GameUtility.Log(this,"da dc bat",Color.green);
    }
    private void Update()
    {
        GiftBG.transform.Rotate(0f,0f,speedRotation*Time.deltaTime);
        undoTxt.text = undoCount.ToString();
        magTxt.text = magnetCount.ToString();
        sortTxt.text = sortCount.ToString();

    }
    public void OnUndoButton()
    {
        if (undoCount <= 0 && PlayerPanelManager.Coin < 300) return;
        else if (undoCount <= 0 && PlayerPanelManager.Coin >= 300)
        {
            PlayerPanelManager.Coin -= 300;
            PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
            GameUtility.Log(this, $"PlayerPanelManager.Coin = {PlayerPanelManager.Coin}", Color.cyan);
            undoCount++;
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
                Destroy(undoCell );
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
        undoCell.transform.DOMove(cell.undoPosition, 0.25f);
        cell.clickable = true;
        //----
        undoCount--;
        PlayerPrefs.SetInt("undoCount",undoCount);
    }
   
    private static void ResetAllCountNumber(CellManager cell)
    {
        DataGame.countAllCell++;
        DataGame.countTickedCell--;
        int indexCell = cell.indexSprite;
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
        i = cell.i;
        j = cell.j;
        layer = cell.layer;
    }
    //-----------------------------------------------------------------------------------------------------------
    public void OnMagnetButton()
    {
        // duyet list ticked cell
        // chon ra cell co so dem lon nhat
        // chon ra cac cell cung loai voi cell tren tu gird
        // set cell.clickable cua cac cell duoc chon la true
        if (magnetCount <= 0 && PlayerPanelManager.Coin < 300) return;
        else if (magnetCount <= 0 && PlayerPanelManager.Coin >= 300)
        {
            PlayerPanelManager.Coin -= 300;
            PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
            GameUtility.Log(this, $"PlayerPanelManager.Coin = {PlayerPanelManager.Coin}", Color.cyan);
            magnetCount++;
        }
        int count = 0;
        int indexSprite = 0;
        GetCountAndIndexSprite(ref count, ref indexSprite);
        Debug.Log($"count : {count} + indexSprite : {indexSprite}");
        GetSameCell(ref count, ref indexSprite);
        //StartCoroutine(ProcessClicks(DataGame.layerGrid,indexSprite,count));
        //----
        magnetCount--;
        PlayerPrefs.SetInt("magnetCount",magnetCount);
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
                        cell.clickable = true;
                        cell.SetUpColor(Color.white);
                        if (count == 0 && indexSprite == 0) indexSprite = cell.indexSprite;

                        if (cell.indexSprite == indexSprite && count < 3 && DataGame.countTickedCell <= 7)
                        {
                            ClickHandler clickCell = boardCell[j, k].GetComponent<ClickHandler>();
                            PointerEventData eventData = new PointerEventData(EventSystem.current);
                            clickCell.OnPointerClick(eventData);
                            //StartCoroutine(Delay(clickCell, eventData));
                            count++;
                        }
                        if (count == 3) return;
                    }
                }
            }
        }
    }
    //IEnumerator ProcessClicks(List<GameObject[,]> board, int indexSprite,int count )
    //{
    //    for (int i = board.Count - 1; i >= 0; i--)
    //    {
    //        GameObject[,] boardCell = board[i];
    //        for (int j = 0; j < boardCell.GetLength(0); j++)
    //        {
    //            for (int k = 0; k < boardCell.GetLength(1); k++)
    //            {
    //                if (boardCell[j, k] != null)
    //                {
    //                    CellManager cell = boardCell[j, k].GetComponent<CellManager>();
    //                    if (cell.indexSprite == indexSprite && count < 3 && DataGame.countTickedCell <= 7)
    //                    {
    //                        if (cell.clickable)
    //                        {
    //                            ClickHandler clickCell = boardCell[j, k].GetComponent<ClickHandler>();
    //                            PointerEventData eventData = new PointerEventData(EventSystem.current);

    //                            // Thực hiện click cell
    //                            clickCell.OnPointerClick(eventData);
    //                            count++;

    //                            // Chờ trước khi click cell tiếp theo
    //                            yield return new WaitForSeconds(0.5f);
    //                        }
    //                    }

    //                    if (count == 3) yield break;
    //                }
    //            }
    //        }
    //    }
    //}

    private static void GetCountAndIndexSprite(ref int count, ref int indexSprite)
    {
        for (int i = 0; i < DataGame.listTickedCell.Length; i++)
        {
            if (DataGame.listTickedCell[i] == null) break;
            CellManager cell = DataGame.listTickedCell[i].GetComponent<CellManager>();
            if (DataGame.arrindex[cell.indexSprite] > count)
            {
                indexSprite = cell.indexSprite;
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
        if (sortCount <= 0 && PlayerPanelManager.Coin < 300) return;
        else if (sortCount <= 0 && PlayerPanelManager.Coin >= 300)
        {
            PlayerPanelManager.Coin -= 300;
            PlayerPrefs.SetInt("coin", PlayerPanelManager.Coin);
            GameUtility.Log(this, $"PlayerPanelManager.Coin = {PlayerPanelManager.Coin}", Color.cyan);
            sortCount++;
        }
        List<GameObject[,]> board = DataGame.layerGrid;
        List<int> tempList = new List<int>();
        ConvertBoardToListIndex(board, tempList);
        RandomSortList(tempList);
        ConvertListIndexToBoard(tempList);
        sortCount--;
        PlayerPrefs.SetInt("sortCount",sortCount);
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
                        tempCell.indexSprite = tempList[index];
                        tempCell.SetUpSprite(tempCell.indexSprite);
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
                        tempList.Add(cell.indexSprite);
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
            return aIndex.indexSprite.CompareTo(bIndex.indexSprite);
        });
    }
    //-----------------------------------------------------------------------------------------------------------
    public void OnGiftButton()
    {
        giftPanel.SetActive(true);
        
    }
}
