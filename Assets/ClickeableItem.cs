using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickeableItem : MonoBehaviour, IPointerClickHandler
{
    private GameManager _gameManager;
    private SpriteRenderer _spriteRenderer;
    private Material _material;

    private Boolean isPair;
    private int assignedSlot; // 0 = left, 1 = right

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

        _material = _spriteRenderer.material;

        _material.SetColor("_OutlineColor", new Color(0, 0, 0, 0));
        _material.SetFloat("_OutlineSize", 0f);
    }

    public void SetAssignedSlot(int slot)
    {
        assignedSlot = slot;
    }

    public int GetAssignedSlot()
    {
        return assignedSlot;
    }
    public Boolean GetPairValue()
    {
        return isPair;
    }

    public void SetDesign(Sprite newSprite, Boolean newPairValue)
    {
        _spriteRenderer.sprite = newSprite;
        isPair = newPairValue;
    }

    public void SetBorder(Boolean border)
    {
        if (border)
        {
            _material.SetColor("_OutlineColor", Color.white); // white border
            _material.SetFloat("_OutlineSize", 1f);           // 1 pixel thick
        }
        else
        {
            _material.SetColor("_OutlineColor", new Color(0, 0, 0, 0)); // hide
            _material.SetFloat("_OutlineSize", 0f);                  // no border
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _gameManager.OnItemClicked(this);
    }
}
