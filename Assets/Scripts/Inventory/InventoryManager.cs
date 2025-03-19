using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;

    public bool isInventoryOpen = false;

    public delegate void InventoryToggled(bool state);
    public event InventoryToggled OnInventoryToggled;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load the Inventory scene additively, but keep it inactive
        SceneManager.LoadSceneAsync("Inventory", LoadSceneMode.Additive).completed += (op) =>
        {
            Scene inventoryScene = SceneManager.GetSceneByName("Inventory");
            foreach (GameObject obj in inventoryScene.GetRootGameObjects())
            {
                obj.SetActive(false); // Keep everything inactive until needed
            }
        };
    }

    public void ToggleInventory()
    {
        if (isInventoryOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        isInventoryOpen = true;
        Scene inventoryScene = SceneManager.GetSceneByName("Inventory");

        foreach (GameObject obj in inventoryScene.GetRootGameObjects())
        {
            obj.SetActive(true); // Activate objects
        }

        // 🔹 Assign ItemContent AFTER the Inventory scene is active
        if (ItemContent == null)
        {
            ItemContent = GameObject.Find("Canvas/Panel/Inventory (Scroll View)/Viewport/Content")?.transform;
            if (ItemContent == null)
            {
                Debug.LogError("InventoryManager: ItemContent not found! Check UI hierarchy.");
                return;
            }
        }

        // 🔹 Load the InventoryItem prefab dynamically from Resources
        if (InventoryItem == null)
        {
            InventoryItem = Resources.Load<GameObject>("Prefabs/UI/InventoryItem");
            if (InventoryItem == null)
            {
                Debug.LogError("InventoryManager: InventoryItem prefab not found! Make sure it's inside Resources/Prefabs/UI/");
                return;
            }
        }

        OnInventoryToggled?.Invoke(isInventoryOpen);
        ListItems();
    }

    public void CloseInventory()
    {
        isInventoryOpen = false;
        Scene inventoryScene = SceneManager.GetSceneByName("Inventory");

        foreach (GameObject obj in inventoryScene.GetRootGameObjects())
        {
            obj.SetActive(false); // Deactivate objects instead of unloading the scene
        }

        OnInventoryToggled?.Invoke(isInventoryOpen);
    }

    public void Add(Item item)
    {
        Items.Add(item);
        ListItems();
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        if (ItemContent == null)
        {
            Debug.LogError("InventoryManager: ItemContent is null. Make sure the Inventory UI is loaded.");
            return;
        }
    
        if (InventoryItem == null)
        {
            Debug.LogError("InventoryManager: InventoryItem is null. Ensure the prefab is loaded.");
            return;
        }
    
        // 🔹 Clear previous UI items before adding new ones
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    
        // 🔹 Dictionary to track item counts
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();
        int allItemsCount = 0;
    
        // 🔹 Count occurrences of each item
        foreach (var item in Items)
        {
            if (itemCounts.ContainsKey(item.itemName))
            {
                itemCounts[item.itemName]++;
            }
            // TODO remove overflow capabilities
            else
            {
                itemCounts[item.itemName] = 1;
                allItemsCount ++;
            }
        }
    
        // 🔹 Instantiate UI elements for unique items and set counts
        foreach (var item in itemCounts)
        {
            string itemName = item.Key;
            int count = item.Value;
    
            // 🔹 Find the first occurrence of this item in the Items list
            Item itemData = Items.Find(i => i.itemName == itemName);
    
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemNameText = obj.transform.Find("ItemName (Text (TMP))").GetComponent<TMP_Text>();
            var itemCounter = obj.transform.Find("Item Counter").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();
    
            itemNameText.text = itemName;
            itemIcon.sprite = itemData.icon;
            itemCounter.text = count.ToString(); // Set the correct count
        }
        int placeholderItems = 30-allItemsCount;
        for (placeholderItems=30 - allItemsCount; placeholderItems>0; placeholderItems--)
        {
            allItemsCount++;
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj.transform.Find("Image").GetComponent<Image>();

            itemIcon.color = Color.clear;
        }
    

    }
}

/*
Most/all (depending on version) of the comments with "🔹" were generated by ChatGPT while debugging on 12.03.25 and underwent no or only minimal
editing.
*/