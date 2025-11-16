using UnityEngine;

[CreateAssetMenu(fileName = "NewIconPack", menuName = "Market/Icon Pack")]
public class IconPack : ScriptableObject
{
  public string packName;
  public int price;
  public Sprite[] iconSprites;           // all icons that belong to this pack

  public Sprite getPreviewIcon()
  {
    if (iconSprites.Length == 0)
    {
      Debug.Log("No se han cargado íconos");
    }
    return iconSprites[0];
  }
}