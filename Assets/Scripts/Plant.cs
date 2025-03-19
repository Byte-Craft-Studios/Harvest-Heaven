using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    // list with all the growth stages
    public Sprite[] growthStages;
    
    // reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;

    // time it takes for the plant to grow to the next stage
    public float growthTime = 5f;
    private int currentStage = 0;
    public float growthOffset = 0f;

    // item that the plant will drop when harvested
    public GameObject itemPrefab;

    // flag to check if the plant is fully grown
    private bool isFullyGrown = false;

    private void Start()
    {
        // get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = growthStages[currentStage];

        // start the growth process
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        // loop through all the growth stages
        while (currentStage < growthStages.Length - 1)
        {
            // wait for the growth time
            yield return new WaitForSeconds(growthTime);

            // increase the current stage
            currentStage++;

            // change the sprite to the next growth stage
            spriteRenderer.sprite = growthStages[currentStage];

            // move the plant up a bit on the y-axis
            transform.position += new Vector3(0, growthOffset, 0);
        }

        // plant is fully grown
        isFullyGrown = true;
    }

    private void Update()
    {
        if (isFullyGrown && Input.GetKeyDown(KeyCode.Space))
        {
            // harvest the plant
            Harvest();
        }
    }

    private void Harvest()
    {
        // create here the item that the plant will drop
        Instantiate(itemPrefab, transform.position, Quaternion.identity);

        // destroy the plant
        Destroy(gameObject);
    }
}
