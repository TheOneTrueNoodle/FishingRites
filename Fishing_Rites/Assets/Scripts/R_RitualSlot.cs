using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_RitualSlot : MonoBehaviour
{
    public ItemType RequiredItem;
    public R_Item Item;

    private Image ItemImage;

    public void AssignItem(R_Item nItem)
    {
        Item = nItem;
        ItemImage.sprite = Item.ItemSprite;
        ItemImage.color = new Color(255, 255, 255, 1);
    }
}
