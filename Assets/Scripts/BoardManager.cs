
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [SerializeField] int layer;
    [SerializeField] GameObject prefabs;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] List<SetUpNumberCell> setUpNumbers;
    
    


    void Start()
    {
        ResetTickedCell();
        
        SetUpBoard(layer);
    }

    void Update()
    {
        for(int i = layer-2; i >=0; i--)
        {
            GameObject[,] currentGrid = TickedCell.layerGrid[i];
            GameObject[,] checkGrid = TickedCell.layerGrid[i + 1];
            for (int j = 1; j < currentGrid.GetLength(0)-1; j++)
            {
                for (int k = 1; k < currentGrid.GetLength(1)-1; k++)
                {
                    GameObject currentCell = currentGrid[j, k];
                    GameObject checkCell1 = checkGrid[j, k];
                    GameObject checkCell2 = checkGrid[j, k - 1];
                    GameObject checkCell3 = checkGrid[j - 1, k];
                    GameObject checkCell4 = checkGrid[j - 1, k - 1];
                    if (checkCell1 == null && checkCell2 == null && checkCell3 == null && checkCell4 == null&&currentCell!=null)
                    {
                        CellManager cell = currentCell.GetComponent<CellManager>();
                        cell.clickable = true;
                        SetUpClickAble(currentCell,cell);
                    }
                }
            }
        }
    }

    void SetUpBoard(int layer)
    {
        for(int z=0; z<layer; z++)
        {
            if (z % 2 == 0)
            {
                width = 8;
                height = 9;
                GameObject[,] grid = new GameObject[10, 11];
                TickedCell.countAllCell += 72;
                Vector3 tickPosition = new Vector3(95f, 963.5f, 0f);
                SpawmCell(tickPosition,z, grid);
                TickedCell.layerGrid.Add(grid);
            }
            else
            {
                width = 7;
                height = 9;
                GameObject[,] grid = new GameObject[9, 11];
                TickedCell.countAllCell += 63;
                Vector3 tickPosition = new Vector3(165f, 1036f, 0f);
                SpawmCell(tickPosition, z,grid);
                TickedCell.layerGrid.Add(grid);
            }
        }
    }

    void SpawmCell(Vector3 tickPosition,int layer, GameObject[,] grid)
    {
        int index = 0;
        for (int i = 1; i <= width; i++)
        {
            for (int j = 1; j <= height; j++)
            {
                index = Random.Range(0, setUpNumbers.Count);

                while (setUpNumbers[index].number == 0)
                {
                    index = Random.Range(0, setUpNumbers.Count);
                }
                //
                Vector3 position = new Vector3((i-1) * 140 + tickPosition.x, (j-1) * 140 + tickPosition.y, 0);
                GameObject cell = Instantiate(prefabs, position, Quaternion.identity);
                CellManager cellSprite = cell.GetComponent<CellManager>();
                cellSprite.layer = layer;
                cellSprite.i = i;
                cellSprite.j=j;
                cellSprite.SetUpSprite(index);
                SetUpClickAble(cell, cellSprite);
                cell.transform.SetParent(transform); // gan doi tuong cell lam con doi tuong board
                grid[i, j] = cell;
                setUpNumbers[index].number--;
            }
        }
    }

    static void SetUpClickAble(GameObject cell, CellManager cellSprite)
    {
        if (cellSprite.clickable)
        {
            Image image = cell.GetComponent<Image>();
            image.color = Color.white;

        }
        else
        {
            Image image = cell.GetComponent<Image>();
            image.color = Color.gray;
        }
    }

    void ResetTickedCell()
    {
        TickedCell.countAllCell = 0;
        TickedCell.layer = layer;
        for(int i=0;i<7;i++)
        {
            TickedCell.arrindex[i]=0;
            TickedCell.listTickedCell[i] = null;
            
        }
        TickedCell.layerGrid.Clear();
    }
}



[System.Serializable]
public class SetUpNumberCell
{
    public Sprite sprite;
    public int number;

}
public static class TickedCell
{
    public static GameObject[] listTickedCell= new GameObject[7];//luu cac cell
    public static List<Vector3> PositionTicked = new List<Vector3>();//luu position cac o
    //public static int[] ticked = new int[7];//luu indexSprite tai cac o
    public static int[] arrindex = new int[7];//mang dem so luong cac cell 
    public static int countAllCell;//luu tong so co cell tren board
    public static int layer;
    public static List<GameObject[,]> layerGrid = new List<GameObject[,]>();
}
