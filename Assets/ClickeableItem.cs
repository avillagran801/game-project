using UnityEngine;
using UnityEngine.EventSystems;

public class ClickeableItem : MonoBehaviour, IPointerClickHandler
{
    public string ItemType;
    private GameManager _gameManager;
    private SpriteRenderer _spriteRenderer;

    // Awake() gets called during initialisation
    void Awake()
    {
        // Find the game manager in the scene
        _gameManager = FindAnyObjectByType<GameManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_gameManager == null)
        {
            Debug.LogError("No GameManager found in the scene!");
        }
    }

    public void Initialize(Sprite newSprite, string newItemType)
    {
        _spriteRenderer.sprite = newSprite;
        ItemType = newItemType;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _gameManager.OnItemClicked(this);
    }
}
