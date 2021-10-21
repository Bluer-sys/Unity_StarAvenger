using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] private bool isUnlimitedCount;

    public string Name => _name;
    public Sprite Icon => _icon;
    public int Price => _price;
    public bool IsUnlimitedCount => isUnlimitedCount;
}
