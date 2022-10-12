using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_InventoryManager : MonoBehaviour
{
    [Header("Inventory List")]
    public List<R_Item> Items;
    [SerializeField] private GameObject InventoryUI;
    [SerializeField] private GameObject ItemParent;
    [SerializeField] private GameObject InventorySlotPrefab;

    [Header("Ritual Slots")]
    public List<R_RitualSlot> RitualSlot;
    public List<GameObject> CurrentSlots = new List<GameObject>();

    private void Start()
    {
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            GameObject Item = Instantiate(InventorySlotPrefab);
            CurrentSlots.Add(Item);
            Item.name = Items[i].ItemName;
            Item.transform.SetParent(ItemParent.transform, false);
            Item.GetComponentInChildren<R_InventoryObject>().SetDetails(Items[i].ItemSprite, Items[i], RitualSlot);
        }
    }

    public void RemoveItems()
    {
        foreach (GameObject slot in CurrentSlots)
        {
            Destroy(slot.gameObject);
        }
        CurrentSlots.Clear();
        UpdateInventory();
    }
}
