using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] DataLevel dataLevel;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform position;

    
    private void OnEnable()
    {
        for(var i = 0; i < dataLevel.levelCount; i++)
        {
            var obj = Instantiate(prefab,new Vector3(),Quaternion.identity, position);
            var item = obj.GetComponent<Item>();
            item.level = i+1;
            item.levelTxt.text = (i + 1).ToString();
            item.levelData = dataLevel.GetDataLevel(i);
        }
    }
   
    public void OnHomeButton()
    {
        SceneManager.LoadScene(0);
    }
}