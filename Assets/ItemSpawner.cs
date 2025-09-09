using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Sprite[] itemSprites;
    public GameObject itemPrefab;
    private int numItems = 6;
    private ClickeableItem[] leftItems;
    private ClickeableItem[] rightItems;

    public void InitializeItems()
    {
        leftItems = new ClickeableItem[numItems];
        rightItems = new ClickeableItem[numItems];

        for (int i = 0; i < numItems; i++)
        {
            GameObject aux = Instantiate(itemPrefab);
            aux.GetComponent<SpriteRenderer>().sortingOrder = 1;

            leftItems[i] = aux.GetComponent<ClickeableItem>();
            leftItems[i].SetAssignedSlot(0);
        }

        for (int i = 0; i < numItems; i++)
        {
            GameObject aux = Instantiate(itemPrefab);
            aux.GetComponent<SpriteRenderer>().sortingOrder = 1;

            rightItems[i] = aux.GetComponent<ClickeableItem>();
            rightItems[i].SetAssignedSlot(1);
        }
    }

    public void SpawnItems(Slot leftSlot, Slot rightSlot)
    {
        AssignItemsDesign();
        AssignItemsPosition(leftSlot, leftItems);
        AssignItemsPosition(rightSlot, rightItems);
    }

    void AssignItemsPosition(Slot slot, ClickeableItem[] items)
    {
        float margin = 0.3f;
        float slotHalfWidth = slot.Size.x / 2f;
        float slotHalfHeight = slot.Size.y / 2f;

        for (int i = 0; i < numItems; i++)
        {
            // 🔹 Apply random rotation (Z axis since it's 2D)
            float randomPossibility = Random.Range(0, 100);
            float rotation = 0f;

            if (randomPossibility > 60)
            {
                rotation = 90f * Random.Range(1, 4); // 90, 180, 270
            }

            items[i].transform.rotation = Quaternion.Euler(0f, 0f, rotation);

            // Since we’re not scaling, use the sprite’s default size
            SpriteRenderer itemSR = items[i].GetComponent<SpriteRenderer>();
            float itemHalfWidth = itemSR.bounds.size.x / 2f;
            float itemHalfHeight = itemSR.bounds.size.y / 2f;

            float maxX = slotHalfWidth - itemHalfWidth - margin;
            float maxY = slotHalfHeight - itemHalfHeight - margin;

            Vector2 localOffset = new Vector2(
                Random.Range(-maxX, maxX),
                Random.Range(-maxY, maxY)
            );

            bool validPos = false;
            int attempts = 0;

            while (!validPos && attempts < 50)
            {
                localOffset = new Vector2(
                    Random.Range(-maxX, maxX),
                    Random.Range(-maxY, maxY)
                );

                validPos = true;

                // Check overlap with already placed items
                for (int j = 0; j < i; j++)
                {
                    Vector2 otherPos = items[j].transform.localPosition;

                    SpriteRenderer otherSR = items[j].GetComponent<SpriteRenderer>();
                    float otherHalfWidth = otherSR.bounds.size.x / 2f;
                    float otherHalfHeight = otherSR.bounds.size.y / 2f;

                    bool overlapX = Mathf.Abs(localOffset.x - otherPos.x) < (itemHalfWidth + otherHalfWidth + margin);
                    bool overlapY = Mathf.Abs(localOffset.y - otherPos.y) < (itemHalfHeight + otherHalfHeight + margin);

                    if (overlapX && overlapY)
                    {
                        validPos = false;
                        break;
                    }
                }

                attempts++;
            }

            // Assign final position
            items[i].transform.SetParent(slot.transform, false);
            items[i].transform.localPosition = localOffset;
        }
    }

    public void AssignItemsDesign()
    {
        int uniqueSpritesNeeded = (numItems * 2) - 1;

        HashSet<int> chosenSpritesSet = new HashSet<int>();

        // Chooses numItems*2 - 1 unique sprite indexes
        while (chosenSpritesSet.Count < uniqueSpritesNeeded)
        {
            int aux = Random.Range(0, itemSprites.Length);
            chosenSpritesSet.Add(aux);
        }

        List<int> chosenIndex = new List<int>(chosenSpritesSet);

        // Assigns all the unique sprites and the duplicates in each slot

        for (int i = 0; i < numItems - 1; i++)
        {
            // 0, ..., numItems - 2
            leftItems[i].SetDesign(itemSprites[chosenIndex[i]], false);

            // numItems - 1, ..., 2*numItems - 3
            rightItems[i].SetDesign(itemSprites[chosenIndex[i + numItems - 1]], false);
        }

        leftItems[numItems - 1].SetDesign(itemSprites[chosenIndex[uniqueSpritesNeeded - 1]], true);
        rightItems[numItems - 1].SetDesign(itemSprites[chosenIndex[uniqueSpritesNeeded - 1]], true);
    }
}
