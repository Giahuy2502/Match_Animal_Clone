using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    [SerializeField] SpriteData sprites;
    [SerializeField] Image animalImage;
    [SerializeField] Image backGroundImage;
    public int indexSprite;
    public int layer;
    public int i;
    public int j;
    public bool clickable;
    public Vector3 undoPosition;

    
    public void SetUpSprite(int index)
    {
        
        this.indexSprite = index;
        animalImage.sprite = DataGame.setUpNumbers[indexSprite].Sprite;

        if(layer == DataGame.layer-1) clickable = true;
        
    }
    public void SetUpColor(Color color)
    {
        animalImage.color = color;
        backGroundImage.color = color;
    }
}
