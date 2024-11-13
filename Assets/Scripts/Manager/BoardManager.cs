
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private int layer;
    [SerializeField] private int countAllCell;
    [SerializeField] private GameObject prefabs;
    [SerializeField] private ScoreManager score;
    [SerializeField] private DataLevel dataLevel;
    [SerializeField] private List<SetUpNumberCell> setUpNumbers=new List<SetUpNumberCell>();
    [SerializeField] private TextAsset csv;
    [SerializeField] private List<List<List<string>>> boardLayer = new List<List<List<string>>>();
    [SerializeField] private RectTransform tickPosition1;
    [SerializeField] private RectTransform tickPosition2;
    [SerializeField] private int cellSize;
    public static int levelCurrent = 1;
    void Start()
    {
        GetDataLevel();
        ResetDataGame();
        ReadCSVFile();
        SetUpGrid(layer);
    }
    void Update()
    {
        CheckClickableCell();
    }
    void GetDataLevel()
    {
        DataLevel cloneData = (DataLevel)dataLevel.Clone();
        bool isLevelFound = false;

        DataGame.countAllCell = 0;
        foreach (var tmp in cloneData.GetListLevels())
        {
            if (tmp.GetLevel() == levelCurrent)
            {
                layer = tmp.GetLayer();
                csv = tmp.GetCSVFile();
                setUpNumbers = new List<SetUpNumberCell>(tmp.GetSetUpNumbers());
                DataGame.countAllCell = tmp.GetCountAllCell();
                score.setMaxScore(DataGame.countAllCell * 10);

                isLevelFound = true;
                break;
            }
        }

        if (!isLevelFound)
        {
            Debug.Log("Level không tồn tại trong danh sách");
        }
    }
    void ReadCSVFile()
    {
        var csvReader = new CsvReader();
        boardLayer = csvReader.ReadCsvLayer(csv.text);      
    }
    void CheckClickableCell()
    {
        CheckClickableCellOnTopGrid();

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
                        CheckClickableOnOddGrid(currentGrid, checkGrid, j, k);
                    }

                    else
                    {
                        CheckClickableOnEvenGrid(currentGrid, checkGrid, j, k);
                    }

                }
            }
        }
    }
    private void CheckClickableOnEvenGrid(GameObject[,] currentGrid, GameObject[,] checkGrid, int j, int k)
    {
        GameObject currentCell = currentGrid[j, k];
        GameObject checkCell1 = checkGrid[j, k];
        GameObject checkCell2 = checkGrid[j, k + 1];
        GameObject checkCell3 = checkGrid[j + 1, k];
        GameObject checkCell4 = checkGrid[j + 1, k + 1];
        CheckConditionsClickableCell(currentCell, checkCell1, checkCell2, checkCell3, checkCell4);
    }
    private void CheckClickableOnOddGrid(GameObject[,] currentGrid, GameObject[,] checkGrid, int j, int k)
    {
        GameObject currentCell = currentGrid[j, k];
        GameObject checkCell1 = checkGrid[j, k];
        GameObject checkCell2 = checkGrid[j, k - 1];
        GameObject checkCell3 = checkGrid[j - 1, k];
        GameObject checkCell4 = checkGrid[j - 1, k - 1];
        CheckConditionsClickableCell(currentCell, checkCell1, checkCell2, checkCell3, checkCell4);
    }
    private void CheckClickableCellOnTopGrid()
    {
        GameObject[,] topGrid = DataGame.layerGrid[layer - 1];
        for (int j = 1; j < topGrid.GetLength(0) - 1; j++)
        {
            for (int k = 1; k < topGrid.GetLength(1) - 1; k++)
            {
                if (topGrid[j, k] != null)
                {
                    CellManager tempCell = topGrid[j, k].GetComponent<CellManager>();
                    tempCell.SetClickable(true);
                    tempCell.SetUpColor(Color.white);
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
                Vector3 tickPosition = tickPosition1.position;
                SpawmCell(tickPosition,z, grid, board);
                DataGame.layerGrid.Add(grid);
            }
            else
            {
                GameObject[,] grid = new GameObject[10, 11];
                List<List<string>> board = boardLayer[z];
                Vector3 tickPosition = tickPosition2.position;
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
                    while (DataGame.setUpNumbers[index].GetNumber() == 0)
                    {
                        index = Random.Range(0, setUpNumbers.Count);
                    }
                    Vector3 position = new Vector3((i - 1) * cellSize + tickPosition.x, (j - 1) * cellSize + tickPosition.y, 0);
                    GameObject cell = Instantiate(prefabs, position, Quaternion.identity);
                    CellManager cellSprite = cell.GetComponent<CellManager>();
                    cellSprite.SetLayer(layer);
                    cellSprite.SetI(i);
                    cellSprite.SetJ(j);
                    cellSprite.SetUpSprite(index);
                    SetUpClickAble(cell, cellSprite);
                    cell.transform.SetParent(transform); // gan doi tuong cell lam con doi tuong board
                    grid[i, j] = cell;
                    cellSprite.SetUndoPosition(position);
                    DataGame.setUpNumbers[index].SetNumber(-1);
                }
                
            }
        }
    }
    static void SetUpClickAble(GameObject cell, CellManager cellSprite)
    {
        if (cellSprite.GetClickable()) cellSprite.SetUpColor(Color.white);
        else cellSprite.SetUpColor(Color.gray);
    }
    void ResetDataGame()
    {
        
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
        DataGame.setUpNumbers = new List<SetUpNumberCell>(setUpNumbers);
        DataGame.arrindex = new int[setUpNumbers.Count];
        
    }
    static void CheckConditionsClickableCell(GameObject currentCell, GameObject checkCell1, GameObject checkCell2, GameObject checkCell3, GameObject checkCell4)
    {
        if (checkCell1 == null && checkCell2 == null && checkCell3 == null && checkCell4 == null && currentCell != null)
        {
            CellManager cell = currentCell.GetComponent<CellManager>();
            cell.SetClickable(true);
            SetUpClickAble(currentCell, cell);
        }
        else
        {
            if (currentCell != null)
            {
                CellManager cell = currentCell.GetComponent<CellManager>();
                cell.SetClickable(false);
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
    [SerializeField] private int number;
    public int GetNumber() => number;
    public void SetNumber(int count)
    {
        number += count;
    }
    public SetUpNumberCell DeepClone()
    {
        // Sử dụng MemberwiseClone nếu chỉ chứa các giá trị đơn giản, 
        // nếu không thì thực hiện sao chép sâu với các đối tượng bên trong.
        return (SetUpNumberCell)this.MemberwiseClone();
    }
}
