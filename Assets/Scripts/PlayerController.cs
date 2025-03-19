using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // player movement speed
    public float moveSpeed;

    // reference to the Rigidbody2D component
    public Rigidbody2D rb;

    // reference to the Animator component
    public Animator animator;

    // reference to the Inventory component
    public Inventory inventory;

    private void Awake()
    {
        // get the components
        inventory = new Inventory(21);
    }

    void AnimateMovement(Vector3 direction)
    {
        // set the animator parameters
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    private void TryPlantSeed()
    {
        // cast a box in front of the player
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.2f, 0, Vector2.zero);

        if (hit.collider != null)
        {
            // check if the player is colliding with a FarmTile
            FarmTile farmTile = hit.collider.GetComponent<FarmTile>();

            if (farmTile != null)
            {
                // plant the seed
                farmTile.PlantSeed();
            }
        }
    }

    private void Update()
    {
        // get the player input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // get the direction the player is moving
        Vector3 direction = new Vector3(horizontal, vertical).normalized;

        // animate the player
        AnimateMovement(direction);

        // move the player
        transform.position += direction * moveSpeed * Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            // toggle the inventory
            InventoryManager.Instance.ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // try to plant a seed
            TryPlantSeed();
        }
    }
}
