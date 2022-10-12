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
    [HideInInspector] public List<GameObject> RitualSlot;
    [SerializeField] float speed = 15f;
    [SerializeField] float LockOnDistance = 1.5f;

    //Item Information
    public R_Item Item;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void SetDetails(Sprite nSprite, R_Item nItem)
    {
        GetComponent<Image>().sprite = nSprite;
        Item = nItem;
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
        /*for(int i = 0; i < RitualSlot.Count; i++)
        {
            if (Vector3.Distance(RitualSlot[i].gameObject.transform.position, GetMousePos()) <= LockOnDistance)
            {
                //Do code to remove from inventory and put into the ritual slot
            }
            else
            {
                transform.position = InventoryPosition;
                GetComponent<Image>().maskable = true;
            }
            transform.position = InventoryPosition;
            GetComponent<Image>().maskable = true;
        }*/
        transform.localPosition = new Vector3(0, 0, 0);
        GetComponent<Image>().maskable = true;
    }
    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
