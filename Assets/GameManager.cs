using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject slotPrefab;
    public ItemSpawner spawner;
    private Slot leftSlot;
    private Slot rightSlot;

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

        spawner.Initialize();
    }

    public void SpawnItemsIntoSlot()
    {
        spawner.SpawnItems(leftSlot, rightSlot);
    }

    public void OnItemClicked(ClickeableItem clickedItem)
    {
        Debug.Log("Clicked item! " + clickedItem.GetEntityId());
    }
}
