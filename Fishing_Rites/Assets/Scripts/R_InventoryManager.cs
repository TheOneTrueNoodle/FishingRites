using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("Huntable Fish")]
    public List<R_Fish> Fish;
    [SerializeField] private Image FishSprite;
    [SerializeField] private Sprite DefaultImage;
    private R_Fish FishToHunt;
    private bool FoundFish;
    public List<R_Fish> DefaultFish;

    private bool InventoryActive = false;
    public string CombatScene;
    public Animator transition;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && InventoryActive == false)
        {
            OpenInventory();
        }
    }

    public void UpdateFishHunt()
    {
        FishSprite.sprite = DefaultImage;
        FoundFish = false;
        FishToHunt = null;

        for (int i = 0; i < Fish.Count; i++)
        {
            int ItemsRequired = Fish[i].RequiredItems.Count;
            for(int j = 0; j < RitualSlot.Count; j++)
            {
                foreach(string Item in Fish[i].RequiredItems)
                {
                    if(RitualSlot[j].Item != null && RitualSlot[j].Item.ItemName == Item)
                    {
                        ItemsRequired--;
                    }
                }
            }
            if(ItemsRequired <= 0)
            {
                FishToHunt = Fish[i];
                FoundFish = true;
                FishSprite.sprite = Fish[i].FishSprite;
                break;
            }
            else
            {
                FishSprite.sprite = DefaultImages;
                FoundFish = false;
                FishToHunt = null;
            }
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            GameObject Item = Instantiate(InventorySlotPrefab);
            CurrentSlots.Add(Item);
            Item.name = Items[i].ItemName;
            Item.transform.SetParent(ItemParent.transform, false);
            Item.GetComponentInChildren<R_InventoryObject>().SetDetails(Items[i].ItemSprite, Items[i], RitualSlot, i);
        }
    }

    public void OpenInventory()
    {
        FindObjectOfType<R_OverworldPlayerMovement>().CanMove = false;
        InventoryUI.SetActive(true);
        InventoryActive = true;

        int NumLoops = RitualSlot.Count;

        for (int i = 0; i < NumLoops; i++)
        {
            RitualSlot[0].RemoveItem();
        }

        NumLoops = CurrentSlots.Count;

        for (int i = 0; i < NumLoops; i++)
        {
            GameObject obj = CurrentSlots[0];
            CurrentSlots.Remove(CurrentSlots[0]);
            Destroy(obj);
        }

        UpdateInventory();
    }
    public void CloseInventroy()
    {
        FindObjectOfType<R_OverworldPlayerMovement>().CanMove = true;
        InventoryUI.SetActive(false);
        InventoryActive = false;

        int NumLoops = RitualSlot.Count;

        for (int i = 0; i < NumLoops; i++)
        {
            RitualSlot[0].RemoveItem();
        }

        NumLoops = CurrentSlots.Count;

        for (int i = 0; i < NumLoops; i++)
        {
            GameObject obj = CurrentSlots[0];
            CurrentSlots.Remove(CurrentSlots[0]);
            Destroy(obj);
        }

        FishSprite.sprite = DefaultImage;
        FoundFish = false;
        FishToHunt = null;
    }

    public void RemoveItem(R_Item Item)
    {
        Items.RemoveAt(Item.ItemNum);

        int NumLoops = CurrentSlots.Count;

        for (int i = 0; i < NumLoops; i++)
        {
            GameObject obj = CurrentSlots[0];
            CurrentSlots.Remove(CurrentSlots[0]);
            Destroy(obj);
        }
        CurrentSlots.Clear();
        UpdateFishHunt();
        UpdateInventory();
    }

    public void AddItem(R_Item Item)
    {
        Items.Add(Item);
        UpdateFishHunt();

        int NumLoops = CurrentSlots.Count;

        for (int i = 0; i < NumLoops; i++)
        {
            GameObject obj = CurrentSlots[0];
            CurrentSlots.Remove(CurrentSlots[0]);
            Destroy(obj);
        }

        CurrentSlots.Clear();
        UpdateInventory();
    }

    public void HuntFish()
    {
        StartCoroutine(LoadCombat());
    }

    IEnumerator LoadCombat()
    {
        if(FoundFish == false)
        {
            FishToHunt = DefaultFish[(int)Random.Range(0, DefaultFish.Count)];
        }

        Scene currentScene = SceneManager.GetActiveScene();
        Destroy(GameObject.Find("EventSystem"));
        Destroy(GameObject.Find("Main Camera"));

        if (transition != null) { transition.SetTrigger("Start"); }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(CombatScene, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        FindObjectOfType<R_FishNoteManager>().LoadFish(FishToHunt);
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
