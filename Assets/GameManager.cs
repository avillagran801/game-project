using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject slotPrefab;
    public ItemSpawner spawner;
    private Slot leftSlot;
    private Slot rightSlot;
    private ClickeableItem leftClickedItem;
    private ClickeableItem rightClickedItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeLayout();
        SpawnItemsIntoSlot();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeLayout()
    {
        GameObject auxSlot = Instantiate(slotPrefab, new Vector2(-4, 0), Quaternion.identity);
        leftSlot = auxSlot.GetComponent<Slot>();

        auxSlot = Instantiate(slotPrefab, new Vector2(4, 0), Quaternion.identity);
        rightSlot = auxSlot.GetComponent<Slot>();

        spawner.InitializeItems();
    }

    public void SpawnItemsIntoSlot()
    {
        spawner.SpawnItems(leftSlot, rightSlot);
    }

    public void OnItemClicked(ClickeableItem clickedItem)
    {
        if (clickedItem.GetAssignedSlot() == 0)
        {
            if (leftClickedItem != null)
            {
                leftClickedItem.SetBorder(false);
            }

            leftClickedItem = clickedItem;
            leftClickedItem.SetBorder(true);
        }
        else
        {
            if (rightClickedItem != null)
            {
                rightClickedItem.SetBorder(false);
            }

            rightClickedItem = clickedItem;
            rightClickedItem.SetBorder(true);
        }

        if (leftClickedItem != null && rightClickedItem != null)
        {
            if (leftClickedItem.GetPairValue() && rightClickedItem.GetPairValue())
            {
                Debug.Log("Pair clicked!");
            }
        }
    }
}
