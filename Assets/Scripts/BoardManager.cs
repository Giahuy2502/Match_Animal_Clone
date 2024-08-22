
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] int layer;
    [SerializeField] GameObject prefabs;
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] List<SetUpNumberCell> setUpNumbers;
    
    GameObject[,] grid = new GameObject[8, 9];

    void Start()
    {
        ResetTickedCell();
        
        SetUpBoard(layer);
    }

    void SetUpBoard(int layer)
    {
        for(int z=0; z<layer; z++)
        {
            if (z % 2 == 0)
            {
                width = 8;
                height = 9;
                TickedCell.countAllCell += 72;
                Vector3 tickPosition = new Vector3(95f, 963.5f, 0f);
                SpawmCell(tickPosition);
            }
            else
            {
                width = 7;
                height = 9;
                TickedCell.countAllCell += 63;
                Vector3 tickPosition = new Vector3(165f, 1036f, 0f);
                SpawmCell(tickPosition);
            }
        }
    }

    void SpawmCell(Vector3 tickPosition)
    {
        int index = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                index = Random.Range(0, setUpNumbers.Count);
                
                while (setUpNumbers[index].number == 0)
                {
                    index = Random.Range(0, setUpNumbers.Count);
                }
                //
                Vector3 position = new Vector3(i * 140 + tickPosition.x, j * 140 + tickPosition.y, 0);
                GameObject cell = Instantiate(prefabs, position, Quaternion.identity);
                CellManager cellSprite = cell.GetComponent<CellManager>();
                cellSprite.SetUpSprite(index);
                cell.transform.SetParent(transform); // gan doi tuong cell lam con doi tuong board
                grid[i, j] = cell;
                setUpNumbers[index].number--;
            }
        }
    }

    void ResetTickedCell()
    {
        TickedCell.countAllCell = 0;
        for(int i=0;i<7;i++)
        {
            TickedCell.arrindex[i]=0;
            TickedCell.listTickedCell[i] = null;
            //TickedCell.ticked[i] = setUpNumbers.Count+1;
        }
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
}
