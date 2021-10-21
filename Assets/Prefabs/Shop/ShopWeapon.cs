using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ShopItem/Weapon", order = 51)]
public class ShopWeapon : ShopItem
{
    [SerializeField] private int _tier;
    [SerializeField] private int _weaponID;

    public int WeaponID => _weaponID;
    public int Tier => _tier;
}
