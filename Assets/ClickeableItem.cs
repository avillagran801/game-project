using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickeableItem : MonoBehaviour, IPointerClickHandler
{
    private GameManager _gameManager;
    private SpriteRenderer _spriteRenderer;
    // private Material _material;

    private Boolean isPair;
    private int assignedSlot; // 0 = left, 1 = right

    // Awake() gets called during initialisation
    void Awake()
    {
        // Find the game manager in the scene
        _gameManager = FindAnyObjectByType<GameManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_material = _spriteRenderer.material;

        if (_gameManager == null)
        {
            Debug.LogError("No GameManager found in the scene!");
        }
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
            //_material.SetColor("_GlowColor", Color.white);
            //_material.SetFloat("_GlowSize", 0.01f); // Start with a small value, adjust in editor
            //_material.SetFloat("_GlowIntensity", 2.0f); // Adjust this for brightness, e.g., 1.5 to 3.0
        }
        else
        {
            //_material.SetColor("_GlowColor", Color.clear);
            //_material.SetFloat("_GlowSize", 0f);
            //_material.SetFloat("_GlowIntensity", 0f);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _gameManager.OnItemClicked(this);
    }
}
