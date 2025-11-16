using UnityEngine;

public class PackRender : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetDesign(Sprite newSprite)
    {
        _spriteRenderer.sprite = newSprite;
    }

}
