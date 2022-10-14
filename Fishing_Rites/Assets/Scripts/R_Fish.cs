using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class R_Fish
{
    public string Name;
    public List<string> RequiredItems;
    public Sprite FishSprite;
    public GameObject FishToFightPrefab;

    public List<R_Note> Notes;
}
