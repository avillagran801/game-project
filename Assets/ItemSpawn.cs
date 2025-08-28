using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public Sprite[] itemSprites;
    public GameObject itemPrefab;
    public Vector2 spawnArea = new Vector2(5, 5);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnItems();
    }

    public void SpawnItems()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector2 spawnPosition = (Vector2)transform.position + new Vector2(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
            );

            GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            Sprite chosenSprite = itemSprites[Random.Range(0, itemSprites.Length)];

            newItem.GetComponent<ClickeableItem>().Initialize(chosenSprite, chosenSprite.name);
        }
    }

}
