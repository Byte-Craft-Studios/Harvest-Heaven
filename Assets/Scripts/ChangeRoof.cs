using UnityEngine;
using UnityEngine.Tilemaps;

public class ToggleRoof : MonoBehaviour
{
    public Tilemap Grid; // Assign your "Roof" Tilemap in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player")) // Check if Player enters
            {
                TilemapRenderer renderer = Grid.GetComponent<TilemapRenderer>();
                if (renderer != null)
                {
                    renderer.enabled = false; // Hide the roof
                }
            }
        }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Check if Player exits
        {
            TilemapRenderer renderer = Grid.GetComponent<TilemapRenderer>();
            if (renderer != null)
            {
                renderer.enabled = true; // Show the roof again
            }
        }
    }
    }