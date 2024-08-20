
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
        SetUpBoard(layer);
        TickedCell.listTickedCell.Clear();
    }

    void SetUpBoard(int layer)
    {
        for(int z=0; z<layer; z++)
        {
            if (z % 2 == 0)
            {
                width = 8;
                height = 9;
                Vector3 tickPosition = new Vector3(95f, 963.5f, 0f);
                SpawmCell(tickPosition);
            }
            else
            {
                width = 7;
                height = 9;
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
                Debug.Log(index);
                while (setUpNumbers[index].number == 0)
                {
                    index = Random.Range(0, setUpNumbers.Count);
                    //Debug.Log(index + "  " + setUpNumbers[index].number);
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
}

[System.Serializable]
public class SetUpNumberCell
{
    public Sprite sprite;
    public int number;

}
public static class TickedCell
{
    public static List<GameObject> listTickedCell= new List<GameObject>();
    public static List<Vector3> PositionTicked = new List<Vector3>();
}
