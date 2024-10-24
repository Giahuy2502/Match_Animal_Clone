
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [SerializeField] int layer;
    [SerializeField] GameObject prefabs;
    [SerializeField] List<SetUpNumberCell> setUpNumbers=new List<SetUpNumberCell>();
    [SerializeField] TextAsset csv;
    [SerializeField] List<List<List<string>>> boardLayer = new List<List<List<string>>>();
    void Start()
    {
        ResetDataGame();
       
        SetUpBoard();
        SetUpGrid(layer);
    }

    void Update()
    {
        CheckClickableCell();
    }
    void SetUpBoard()
    {
        var csvReader = new CsvReader();
        boardLayer = csvReader.ReadCsvLayer(csv.text);
        
    }
    void CheckClickableCell()
    {
        GameObject[,] topGrid = DataGame.layerGrid[layer-1];
        for (int j = 1; j < topGrid.GetLength(0) - 1; j++)
        {
            for (int k = 1; k < topGrid.GetLength(1) - 1; k++)
            {
                if (topGrid[j, k] != null)
                {
                    CellManager tempCell = topGrid[j, k].GetComponent<CellManager>();
                    tempCell.clickable = true;
                    tempCell.SetUpColor(Color.white);
                }
            }
        }

        //Debug.Log("Da xet cac cellable");
        for (int i = layer - 2; i >= 0; i--)
        {
            GameObject[,] currentGrid = DataGame.layerGrid[i];
            GameObject[,] checkGrid = DataGame.layerGrid[i + 1];
            for (int j = 1; j < currentGrid.GetLength(0) - 1; j++)
            {
                for (int k = 1; k < currentGrid.GetLength(1) - 1; k++)
                {
                    if (i % 2 == 0)
                    {
                        GameObject currentCell = currentGrid[j, k];
                        GameObject checkCell1 = checkGrid[j, k];
                        GameObject checkCell2 = checkGrid[j, k - 1];
                        GameObject checkCell3 = checkGrid[j - 1, k];
                        GameObject checkCell4 = checkGrid[j - 1, k - 1];
                        CheckConditionsClickableCell(currentCell, checkCell1, checkCell2, checkCell3, checkCell4);
                    }

                    else
                    {
                        GameObject currentCell = currentGrid[j, k];
                        GameObject checkCell1 = checkGrid[j, k];
                        GameObject checkCell2 = checkGrid[j, k + 1];
                        GameObject checkCell3 = checkGrid[j+1, k];
                        GameObject checkCell4 = checkGrid[j+1, k + 1];
                        CheckConditionsClickableCell(currentCell, checkCell1, checkCell2, checkCell3, checkCell4);
                    }
                    
                }
            }
        }
    }

    void SetUpGrid(int layer)
    {
        for(int z=0; z<layer; z++)
        {
            if (z % 2 == 0)
            {
                GameObject[,] grid = new GameObject[10, 11];
                List<List<string>> board = boardLayer[z];
                DataGame.countAllCell += 72;
                Vector3 tickPosition = new Vector3(165f, 665f, 0f);
                SpawmCell(tickPosition,z, grid, board);
                DataGame.layerGrid.Add(grid);
            }
            else
            {
                GameObject[,] grid = new GameObject[10, 11];
                List<List<string>> board = boardLayer[z];
                DataGame.countAllCell += 63;
                Vector3 tickPosition = new Vector3(220f, 720f, 0f);
                SpawmCell(tickPosition, z,grid,board);
                DataGame.layerGrid.Add(grid);
            }
        }
    }
    void SpawmCell(Vector3 tickPosition, int layer, GameObject[,] grid, List<List<string>> board)
    {
        
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                
                if (board[j][i] == "1")
                {
                    int index = Random.Range(0, setUpNumbers.Count);
                    while (setUpNumbers[index].number == 0)
                    {
                        index = Random.Range(0, setUpNumbers.Count);
                    }
                    Vector3 position = new Vector3((i - 1) * 110 + tickPosition.x, (j - 1) * 110 + tickPosition.y, 0);
                    GameObject cell = Instantiate(prefabs, position, Quaternion.identity);
                    CellManager cellSprite = cell.GetComponent<CellManager>();
                    cellSprite.layer = layer;
                    cellSprite.i = i;
                    cellSprite.j = j;
                    cellSprite.SetUpSprite(index);
                    SetUpClickAble(cell, cellSprite);
                    cell.transform.SetParent(transform); // gan doi tuong cell lam con doi tuong board
                    grid[i, j] = cell;
                    cellSprite.undoPosition = position;
                    setUpNumbers[index].number--;
                }
                
            }
        }
    }
    static void SetUpClickAble(GameObject cell, CellManager cellSprite)
    {
        if (cellSprite.clickable) cellSprite.SetUpColor(Color.white);
        else cellSprite.SetUpColor(Color.gray);
    }

    void ResetDataGame()
    {
        DataGame.countAllCell = 0;
        DataGame.layer = layer;
        DataGame.layerGrid.Clear();
        DataGame.stateCurrentPlay = 0;
        DataGame.countTickedCell = 0;
        DataGame.setUpNumbers.Clear();
        for (int i = 0; i < 7; i++)
        {
            DataGame.listTickedCell[i] = null;
        }
        DataGame.undoCell.Clear();
        DataGame.setUpNumbers = setUpNumbers;
        DataGame.arrindex = new int[setUpNumbers.Count];
    }
    static void CheckConditionsClickableCell(GameObject currentCell, GameObject checkCell1, GameObject checkCell2, GameObject checkCell3, GameObject checkCell4)
    {
        if (checkCell1 == null && checkCell2 == null && checkCell3 == null && checkCell4 == null && currentCell != null)
        {
            CellManager cell = currentCell.GetComponent<CellManager>();
            cell.clickable = true;
            SetUpClickAble(currentCell, cell);
        }
        else
        {
            if (currentCell != null)
            {
                CellManager cell = currentCell.GetComponent<CellManager>();
                cell.clickable = false;
                SetUpClickAble(currentCell, cell);
            }
            
        }
    }

}
public static class DataGame
{
    public static GameObject[] listTickedCell= new GameObject[7];//luu cac cell
    public static List<Vector3> PositionTicked = new List<Vector3>();//luu position cac o
    public static int[] arrindex;//mang dem so luong cac cell 
    public static int countAllCell;//luu tong so co cell tren board
    public static int layer;
    public static List<GameObject[,]> layerGrid = new List<GameObject[,]>();
    public static int stateCurrentPlay = 0;
    public static int countTickedCell = 0;
    public static Stack<GameObject> undoCell = new Stack<GameObject>();
    public static List<SetUpNumberCell> setUpNumbers=new List<SetUpNumberCell>();
}


[System.Serializable]
public class SetUpNumberCell
{
    public Sprite Sprite;
    public int number;
}
