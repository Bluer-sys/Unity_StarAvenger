using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : ObjectPool
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _bulletCapacity;
    [SerializeField] private Transform _container;

    private void Awake()
    {
        Initialize(_bulletPrefab, _bulletCapacity, _container);
    }
}
