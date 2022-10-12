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
    public List<R_RitualSlot> RitualSlot;
    [SerializeField] float speed = 15f;

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

    public void SetDetails(Sprite nSprite, R_Item nItem, List<R_RitualSlot> RitualSlots)
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
            if (rectOverlaps(gameObject.GetComponent<RectTransform>(), RitualSlot[i].gameObject.GetComponent<RectTransform>()))
            {
                if(RitualSlot[i].RequiredItem == Item.Type)
                {
                    //Do code to remove from inventory and put into the ritual slot
                    IM.Items.Remove(Item);
                    IM.CurrentSlots.Remove(transform.parent.gameObject);
                    RitualSlot[i].AssignItem(Item);
                    IM.RemoveItems();
                    Destroy(transform.parent.gameObject);
                }
            }
            else
            {
                transform.localPosition = new Vector3(0, 0, 0);
                GetComponent<Image>().maskable = true;
            }
        }
    }

    bool rectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
