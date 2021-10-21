using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ShopItemRender))]
public class PurchasingWeapon : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private AudioSource _buySound;
    [SerializeField] private AudioSource _errorSound;
    [SerializeField] private ShopWeapon[] _weapons;

    private ShopItemRender itemRender;
    private ShopWeapon currentWeaponForSale;
    private int currentTier;

    public UnityAction<int, int> MoneyChanged;
    public UnityAction<int> TierChanged;

    private void Awake()
    {
        itemRender = GetComponent<ShopItemRender>();
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyItem);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyItem);
    }

    public void InitializeWeaponForSale()
    {
        if (currentTier > _weapons.Length - 1)
        {
            itemRender.RenderNothing(_weapons[0].Name, _weapons[0].Icon);
            _buyButton.interactable = false;
            return;
        }

        currentWeaponForSale = _weapons[currentTier];
        itemRender.Render(currentWeaponForSale.Name, currentWeaponForSale.Icon, currentWeaponForSale.Price);
    }

    public void SetShip()
    {
        currentTier = ShopSceneController.PlayerData.shipModel;
    }

    public void SetSimpleWeapon()
    {
        currentTier = ShopSceneController.PlayerData.simpleWeaponTier;
    }

    public void SetTripleWeapon()
    {
        currentTier = ShopSceneController.PlayerData.tripleWeaponTier;
    }

    public void SetLazerWeapon()
    {
        currentTier = ShopSceneController.PlayerData.lazerTier;
    }

    public void SetHomingWeapon()
    {
        currentTier = ShopSceneController.PlayerData.homingWeaponTier;
    }

    private void OnBuyItem()
    {
        if(ShopSceneController.PlayerData.money >= currentWeaponForSale.Price)
        {
            int oldMoney = ShopSceneController.PlayerData.money;
            int newMoney = oldMoney - currentWeaponForSale.Price;

            ShopSceneController.PlayerData.money = newMoney;
            MoneyChanged?.Invoke(oldMoney, newMoney);
            TierChanged?.Invoke(currentWeaponForSale.WeaponID);

            currentTier++;
            InitializeWeaponForSale();

            _buySound.Play();
        }
        else
        {
            _errorSound.Play();
        }
    }  
}
