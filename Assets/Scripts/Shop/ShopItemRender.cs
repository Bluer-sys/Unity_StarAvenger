using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemRender : MonoBehaviour
{
    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _itemPrice;

    public void Render(string itemName, Sprite itemSprite, int itemPrice)
    {
        _itemName.text = itemName;
        _itemImage.sprite = itemSprite;
        _itemPrice.text = itemPrice.ToString();
    }

    internal void RenderNothing(string itemName, Sprite itemSprite)
    {
        _itemName.text = itemName;
        _itemImage.sprite = itemSprite;
        _itemPrice.text = "SOLD";
    }
}
