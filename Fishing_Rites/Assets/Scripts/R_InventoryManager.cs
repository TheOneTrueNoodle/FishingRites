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
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            CloseInventroy();
        }
    }

    public void UpdateFishHunt()
    {
        for(int i = 0; i < Fish.Count; i++)
        {
            int ItemsRequired = Fish[i].RequiredItems.Count;
            for(int j = 0; j < RitualSlot.Count; j++)
            {
                foreach(string Item in Fish[i].RequiredItems)
                {
                    if(RitualSlot[j].Item.ItemName == Item)
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
        UpdateInventory();
    }
    public void CloseInventroy()
    {
        FindObjectOfType<R_OverworldPlayerMovement>().CanMove = true;
        InventoryUI.SetActive(false);
        InventoryActive = false;

        foreach (R_RitualSlot rSlot in RitualSlot)
        {
            rSlot.RemoveItem();
        }

        foreach (GameObject slot in CurrentSlots)
        {
            CurrentSlots.Remove(slot);
            Destroy(slot);
        }
        FishToHunt.FishSprite = null;
        FoundFish = false;
        FishToHunt = null;
    }

    public void RemoveItem(R_Item Item)
    {
        Items.RemoveAt(Item.ItemNum);
        foreach (GameObject slot in CurrentSlots)
        {
            Destroy(slot);
        }
        CurrentSlots.Clear();
        UpdateInventory();
        UpdateFishHunt();
    }

    public void AddItem(R_Item Item)
    {
        Items.Add(Item);
        foreach (GameObject slot in CurrentSlots)
        {
            Destroy(slot);
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
