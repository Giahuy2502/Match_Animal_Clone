
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [SerializeField] int layer;
    [SerializeField] GameObject prefabs;
    //[SerializeField] int width;
    //[SerializeField] int height;
    [SerializeField] List<SetUpNumberCell> setUpNumbers;
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
        //Debug.Log(csv.text);
    }
    void CheckClickableCell()
    {
        
        for (int i = layer - 2; i >= 0; i--)
        {
            GameObject[,] currentGrid = DataGame.layerGrid[i];
            GameObject[,] checkGrid = DataGame.layerGrid[i + 1];
            for (int j = 1; j < currentGrid.GetLength(0) - 1; j++)
            {
                for (int k = 1; k < currentGrid.GetLength(1) - 1; k++)
                {
                    GameObject currentCell = currentGrid[j, k];
                    GameObject checkCell1=new GameObject();
                    GameObject checkCell2 = new GameObject();
                    GameObject checkCell3 = new GameObject();
                    GameObject checkCell4 = new GameObject();
                    if (i%2==0)
                    {
                        checkCell1 = checkGrid[j, k];
                        checkCell2 = checkGrid[j, k - 1];
                        checkCell3 = checkGrid[j - 1, k];
                        checkCell4 = checkGrid[j - 1, k - 1];
                    }
                    else
                    {
                        checkCell1 = checkGrid[j, k];
                        checkCell2 = checkGrid[j, k + 1];
                        checkCell3 = checkGrid[j+1, k];
                        checkCell4 = checkGrid[j+1, k + 1];
                    }
                    if (checkCell1 == null && checkCell2 == null && checkCell3 == null && checkCell4 == null && currentCell != null)
                    {
                        CellManager cell = currentCell.GetComponent<CellManager>();
                        cell.clickable = true;
                        SetUpClickAble(currentCell, cell);
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
                //width = 8;
                //height = 9;
                GameObject[,] grid = new GameObject[10, 11];
                List<List<string>> board = boardLayer[z];
                //Debug.Log(board.Count + "   " + board[0].Count);
                //Debug.Log(grid.GetLength(0) + "   " + grid.GetLength(1));
                DataGame.countAllCell += 72;
                Vector3 tickPosition = new Vector3(95f, 963.5f, 0f);
                SpawmCell(tickPosition,z, grid, board);
                DataGame.layerGrid.Add(grid);
                //Debug.Log("Da add grid 1");
            }
            else
            {
                //width = 7;
                //height = 9;
                GameObject[,] grid = new GameObject[10, 11];
                List<List<string>> board = boardLayer[z];
                Debug.Log(board.Count + "   " + board[0].Count);
                Debug.Log(grid.GetLength(0) + "   " + grid.GetLength(1));
                DataGame.countAllCell += 63;
                Vector3 tickPosition = new Vector3(165f, 1036f, 0f);
                SpawmCell(tickPosition, z,grid,board);
                DataGame.layerGrid.Add(grid);
                //Debug.Log("Da add grid 2");
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
                    //Debug.Log("Da gap TH =1");
                    int index = Random.Range(0, setUpNumbers.Count);
                    while (setUpNumbers[index].number == 0)
                    {
                        index = Random.Range(0, setUpNumbers.Count);
                    }
                    Vector3 position = new Vector3((i - 1) * 140 + tickPosition.x, (j - 1) * 140 + tickPosition.y, 0);
                    GameObject cell = Instantiate(prefabs, position, Quaternion.identity);
                    CellManager cellSprite = cell.GetComponent<CellManager>();
                    cellSprite.layer = layer;
                    cellSprite.i = i;
                    cellSprite.j = j;
                    cellSprite.SetUpSprite(index);
                    SetUpClickAble(cell, cellSprite);
                    cell.transform.SetParent(transform); // gan doi tuong cell lam con doi tuong board
                    grid[i, j] = cell;
                    setUpNumbers[index].number--;
                }
                //else
                //{
                //    grid[i, j] = null;
                //    Debug.Log("da gap TH == 0");
                //}
                //else
                //{
                //    grid[i, j] = null;
                //    Debug.Log(board[j][i]+""+j+"   "+i);
                //}
            }
        }
    }

    static void SetUpClickAble(GameObject cell, CellManager cellSprite)
    {
        Image image = cell.GetComponent<Image>();
        if (cellSprite.clickable) image.color = Color.white;
        else image.color = Color.gray;
    }

    void ResetDataGame()
    {
        DataGame.countAllCell = 0;
        DataGame.layer = layer;
        DataGame.layerGrid.Clear();
        DataGame.stateCurrentPlay = 0;
        DataGame.countTickedCell = 0;
        for (int i = 0; i < 7; i++)
        {
            DataGame.arrindex[i] = 0;
            DataGame.listTickedCell[i] = null;
        }
    }
}
public static class DataGame
{
    public static GameObject[] listTickedCell= new GameObject[7];//luu cac cell
    public static List<Vector3> PositionTicked = new List<Vector3>();//luu position cac o
    public static int[] arrindex = new int[7];//mang dem so luong cac cell 
    public static int countAllCell;//luu tong so co cell tren board
    public static int layer;
    public static List<GameObject[,]> layerGrid = new List<GameObject[,]>();
    public static int stateCurrentPlay = 0;
    public static int countTickedCell = 0;
    
}

[System.Serializable]
public class SetUpNumberCell
{
    public Sprite sprite;
    public int number;

}
