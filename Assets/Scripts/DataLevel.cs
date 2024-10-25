using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataLevel",menuName = "DataLevel")]
public class DataLevel : ScriptableObject
{
    [SerializeField] public List<LevelDesign> DataLv= new List<LevelDesign> ();
}

[Serializable]
public class LevelDesign
{
    [SerializeField] private int lv;
    [SerializeField] private TextAsset csvFile;
    [SerializeField] private int layer;
    [SerializeField] private List<SetUpNumberCell> setUpNumbers;
    public int GetLv() => lv;
    public int GetLayer() => layer;
    public TextAsset GetCsvFile() => csvFile;
    public List<SetUpNumberCell> GetSetUpNumber() => setUpNumbers;

}