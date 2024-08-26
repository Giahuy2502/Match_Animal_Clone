using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    [SerializeField] SpriteData sprites;
    [SerializeField] Image image;
    public int indexSprite;
    public int layer;
    public int i;
    public int j;
    public bool clickable;

    void Start()
    {
        image = GetComponent<Image>();
        //SetUpSprite();
    }
    public void SetUpSprite(int index)
    {
        
        this.indexSprite = index;
        image.sprite = sprites.sprite[indexSprite];

        if(layer == TickedCell.layer-1) clickable = true;
        
    }
}
