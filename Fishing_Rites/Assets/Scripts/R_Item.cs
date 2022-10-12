using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class R_Item
{
    public string ItemName;
    public ItemType Type;
    public Sprite ItemSprite;
}

public enum ItemType
{
    Fish,
    Plant,
}