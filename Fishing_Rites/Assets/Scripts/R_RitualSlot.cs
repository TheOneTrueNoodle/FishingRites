using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_RitualSlot : MonoBehaviour
{
    public ItemType RequiredItem;
    public R_Item Item;
    public bool HasItem;

    [SerializeField] private Image ItemImage;
    private R_InventoryManager IM;
    [SerializeField] private GameObject RemoveItemButton;

    private void Start()
    {
        IM = FindObjectOfType<R_InventoryManager>();
    }

    public void AssignItem(R_Item nItem)
    {
        Item = nItem;
        ItemImage.sprite = nItem.ItemSprite;
        ItemImage.color = new Color(255, 255, 255, 1);
        HasItem = true;
        RemoveItemButton.SetActive(true);
    }

    public void RemoveItem()
    {
        if(HasItem != false)
        {
            IM.AddItem(Item);
            Item = null;
            ItemImage.sprite = null;
            ItemImage.color = new Color(255, 255, 255, 0);
            HasItem = false;
            RemoveItemButton.SetActive(false);
        }
    }
}
