using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
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
        if (!isInventoryOpen)
        {
            isInventoryOpen = true;
            Scene inventoryScene = SceneManager.GetSceneByName("Inventory");
            foreach (GameObject obj in inventoryScene.GetRootGameObjects())
            {
                obj.SetActive(true); // Activate objects
            }
            OnInventoryToggled?.Invoke(isInventoryOpen);
        }
    }

    public void CloseInventory()
    {
        if (isInventoryOpen)
        {
            isInventoryOpen = false;
            Scene inventoryScene = SceneManager.GetSceneByName("Inventory");
            foreach (GameObject obj in inventoryScene.GetRootGameObjects())
            {
                obj.SetActive(false); // Deactivate objects instead of unloading the scene
            }
            OnInventoryToggled?.Invoke(isInventoryOpen);
        }
    }
}
