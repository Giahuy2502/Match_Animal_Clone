using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataLevel",menuName = "DataLevel")]

public class DataLevel : ScriptableObject,ICloneable
{
    [SerializeField] private List<Level> levels;
    public int levelCount => levels.Count;


    public List<Level> GetListLevels() =>levels;
    private List<Level> Levels => levels;
    public Level GetDataLevel(int id) => Levels[id];

    public object Clone()
    {
        DataLevel clonedDataLevel = (DataLevel)this.MemberwiseClone();

        // Sao chép sâu danh sách levels
        clonedDataLevel.levels = new List<Level>();
        foreach (Level level in this.levels)
        {
            clonedDataLevel.levels.Add(level.DeepClone());
        }

        return clonedDataLevel;
    }
}
[Serializable]
public class Level
{
    [SerializeField]private int level;
    [SerializeField] private int layer;
    [SerializeField] private TextAsset csvFile;
    [SerializeField] private int countAllCell;
    [SerializeField] private List<SetUpNumberCell> setUpNumbers;
    public int GetCountAllCell() => countAllCell;
    public int GetLevel() => level;
    public int GetLayer() => layer;
    public TextAsset GetCSVFile() => csvFile;
    public List<SetUpNumberCell> GetSetUpNumbers() => setUpNumbers;
    public Level DeepClone()
    {
        // Tạo bản sao nông của Level
        Level clonedLevel = (Level)this.MemberwiseClone();

        // Sao chép sâu danh sách setUpNumbers
        clonedLevel.setUpNumbers = new List<SetUpNumberCell>();
        foreach (SetUpNumberCell cell in this.setUpNumbers)
        {
            clonedLevel.setUpNumbers.Add(cell.DeepClone());
        }

        return clonedLevel;
    }

}
