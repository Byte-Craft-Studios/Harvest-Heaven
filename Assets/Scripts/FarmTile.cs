using UnityEngine;

public class FarmTile : MonoBehaviour
{
    // flag to check if the tile is already planted
    public bool isPlanted = false;

    // reference to the plant prefab
    public GameObject plantPrefab;

    public void PlantSeed()
    {
        if (!isPlanted)
        {
            // get the center of the collider bounds
            Vector3 spawnPosition = GetComponent<Collider2D>().bounds.center;
            spawnPosition.z = 0;

            // instantiate the plant prefab
            Instantiate(plantPrefab, spawnPosition, Quaternion.identity);

            // set the flag to true
            isPlanted = true;
        }
    }
}
