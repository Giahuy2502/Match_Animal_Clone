using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    [SerializeField] SpriteData sprites;
    [SerializeField] Image image;
    public int index;

    void Start()
    {
        image = GetComponent<Image>();
        //SetUpSprite();
    }
    public void SetUpSprite(int index)
    {
        //index = Random.Range(0, sprites.sprite.Count);
        this.index = index;
        image.sprite = sprites.sprite[index];
    }
}
