using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_InventoryObject : MonoBehaviour
{
    //Drag and drop values
    [Header("Drag and Drop values")]
    private Vector3 dragOffset;
    private Camera cam;
    public List<GameObject> RitualSlot;
    [SerializeField] float speed = 15f;
    [SerializeField] float LockOnDistance = 1.5f;

    private R_InventoryManager IM;

    //Item Information
    public R_Item Item;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        IM = FindObjectOfType<R_InventoryManager>();
    }

    public void SetDetails(Sprite nSprite, R_Item nItem, List<GameObject> RitualSlots)
    {
        GetComponent<Image>().sprite = nSprite;
        Item = nItem;
        RitualSlot = RitualSlots;
    }

    private void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
        GetComponent<Image>().maskable = false;
    }

    private void OnMouseDrag()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);
    }

    private void OnMouseUp()
    {
        for(int i = 0; i < RitualSlot.Count; i++)
        {
            if (Vector3.Distance(RitualSlot[i].transform.position, GetMousePos()) <= LockOnDistance)
            {
                //Do code to remove from inventory and put into the ritual slot
                IM.Items.Remove(Item);
                IM.UpdateInventory();
            }
            else
            {
                transform.localPosition = new Vector3(0, 0, 0);
                GetComponent<Image>().maskable = true;
            }
        }
    }
    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
