using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentMoney;
    [SerializeField] private PurchasingWeapon[] _purchasingWeapons;
    [SerializeField] private Button _resetPurchasesButton;

    private Coroutine moneyChanger;

    private void OnEnable()
    {
        foreach (var weapon in _purchasingWeapons)
        {
            weapon.MoneyChanged += OnMoneyChanged;
            weapon.TierChanged += OnTierChanged;
        }
    }

    private void OnDisable()
    {
        foreach (var weapon in _purchasingWeapons)
        {
            weapon.MoneyChanged -= OnMoneyChanged;
            weapon.TierChanged -= OnTierChanged;
        }
    }

    private void Start()
    {
        SetShop();
    }

    private void SetShop()
    {
        _purchasingWeapons[0].SetShip();
        _purchasingWeapons[0].InitializeWeaponForSale();

        _purchasingWeapons[1].SetSimpleWeapon();
        _purchasingWeapons[1].InitializeWeaponForSale();

        _purchasingWeapons[2].SetTripleWeapon();
        _purchasingWeapons[2].InitializeWeaponForSale();

        _purchasingWeapons[3].SetLazerWeapon();
        _purchasingWeapons[3].InitializeWeaponForSale();

        _purchasingWeapons[4].SetHomingWeapon();
        _purchasingWeapons[4].InitializeWeaponForSale();
    }

    private void OnMoneyChanged(int oldMoney, int newMoney)
    {
        if (moneyChanger != null)
        {
            _currentMoney.text = oldMoney.ToString();
            StopCoroutine(moneyChanger);
        }

        if(!_resetPurchasesButton.gameObject.activeSelf)
            _resetPurchasesButton.gameObject.SetActive(true);

        moneyChanger = StartCoroutine(MoneyChangeAnimation(oldMoney, newMoney));
    }

    private void OnTierChanged(int weaponID)
    {
        switch (weaponID)
        {
            case 0:
                ShopSceneController.PlayerData.shipModel++;
                break;
            case 1:
                ShopSceneController.PlayerData.simpleWeaponTier++;
                break;
            case 2:
                ShopSceneController.PlayerData.tripleWeaponTier++;
                break;
            case 3:
                ShopSceneController.PlayerData.lazerTier++;
                break;
            case 4:
                ShopSceneController.PlayerData.homingWeaponTier++;
                break;
            default:
                break;
        }
    }

    private IEnumerator MoneyChangeAnimation(int oldMoney, int newMoney)
    {
        int money = oldMoney;
        int step = Mathf.Abs(newMoney - oldMoney) / 25;

        while(money != newMoney)
        {
            money -= step;
            _currentMoney.text = money.ToString();
            yield return null;
        }
        _currentMoney.text = newMoney.ToString();

        moneyChanger = null;
    }
}
