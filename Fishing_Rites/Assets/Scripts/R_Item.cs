using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class R_Item
{
    public string ItemName;
    public ItemType Type;
    public Sprite ItemSprite;
    public int ItemNum;

    public object GameObject { get; internal set; }
}

public enum ItemType
{
    Fish,
    Plant,
}