using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewSpriteData", menuName = "SpriteData", order = 1)]
public class SpriteData : ScriptableObject
{
    public List<Sprite> sprite;

}
