using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    [SerializeField] private SpriteData sprites;
    [SerializeField] private Image animalImage;
    [SerializeField] private Image backGroundImage;
    private int indexSprite;
    private int layer;
    private int i;
    private int j;
    private bool clickable;
    private Vector3 undoPosition;

    public int GetI() { return i; }
    public int GetIndexSprite() {  return indexSprite; }
    public int GetJ() { return j; }
    public int GetLayer() { return layer; }
    public bool GetClickable() {  return clickable; }
    public Vector3 GetUndoPosition() { return undoPosition; }
    public void SetLayer(int layer) {  this.layer = layer; }
    public void SetI(int i) { this.i = i;}
    public void SetJ(int j) {  this.j = j;}
    public void SetClickable(bool clickable) {  this.clickable = clickable; }
    public void SetUndoPosition(Vector3 undoPosition) { this.undoPosition = undoPosition;}
    public void SetIndexSprite(int indexSprite) {  this.indexSprite = indexSprite; }

    public void SetUpSprite(int index)
    {      
        this.indexSprite = index;
        animalImage.sprite = DataGame.setUpNumbers[indexSprite].Sprite;      
    }
    public void SetUpColor(Color color)
    {
        animalImage.color = color;
        backGroundImage.color = color;
    }
}
