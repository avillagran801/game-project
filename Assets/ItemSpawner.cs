using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Sprite[] itemSprites;
    public GameObject itemPrefab;
    private int numItems = 5;
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
        float margin = 0.2F;
        float slotHalfWidth = slot.Size.x / 2f;
        float slotHalfHeight = slot.Size.y / 2f;

        for (int i = 0; i < numItems; i++)
        {
            SpriteRenderer itemSR = items[i].GetComponent<SpriteRenderer>();
            float itemHalfWidth = itemSR.bounds.size.x / 2f;
            float itemHalfHeight = itemSR.bounds.size.y / 2f;

            float maxX = slotHalfWidth - itemHalfWidth - margin;
            float maxY = slotHalfHeight - itemHalfHeight - margin;

            Vector2 localOffset = new Vector2(
                Random.Range(-maxX, maxX),
                Random.Range(-maxY, maxY)
            );

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
