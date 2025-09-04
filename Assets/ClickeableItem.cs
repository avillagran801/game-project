using UnityEngine;
using UnityEngine.EventSystems;

public class ClickeableItem : MonoBehaviour, IPointerClickHandler
{
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

    public void SetDesign(Sprite newSprite)
    {
        _spriteRenderer.sprite = newSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _gameManager.OnItemClicked(this);
    }
}
