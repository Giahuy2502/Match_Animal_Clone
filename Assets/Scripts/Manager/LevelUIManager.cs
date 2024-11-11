using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] DataLevel dataLevel;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform position;
    AudioSourceManager audioSourceManager => AudioSourceManager.Instance;

    private void OnEnable()
    {
        for(var i = 0; i < dataLevel.levelCount; i++)
        {
            InitButton(i);
        }
    }

    private void InitButton(int i)
    {
        var obj = Instantiate(prefab, new Vector3(), Quaternion.identity, position);
        var item = obj.GetComponent<Item>();
        item.level = i + 1;
        item.levelTxt.text = (i + 1).ToString();
        item.levelData = dataLevel.GetDataLevel(i);
    }

    
    public void OnSoundButton(int index)
    {
        audioSourceManager.PlayAudio(index);
    }
}