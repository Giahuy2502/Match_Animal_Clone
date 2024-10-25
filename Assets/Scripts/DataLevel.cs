using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataLevel",menuName = "DataLevel")]
public class DataLevel : ScriptableObject
{
    [SerializeField] public List<Level> levels = new List<Level>();
}
[Serializable]
public class Level
{
    [SerializeField] private int level;
    [SerializeField] private int layer;
    [SerializeField] private TextAsset csvFile;
    [SerializeField] private List<SetUpNumberCell> setUpNumbers;

    public int GetLevel() => level;
    public int GetLayer() => layer;
    public TextAsset GetCSVFile() => csvFile;
    public List<SetUpNumberCell> GetSetUpNumbers() => setUpNumbers;
}
