using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player)
        {
            InventoryManager.Instance.Add(Item);
            Destroy(gameObject);
        }
    }
}
