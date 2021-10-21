using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : Weapon
{
    [SerializeField] protected string _name;
    [SerializeField] protected Sprite _icon;
    [SerializeField] protected int _price;
    [SerializeField] protected PlayerBulletPool _bulletContainer;
    [SerializeField] protected Transform _shootPoint;
    [SerializeField] protected float _baseShootInterval;

    [SerializeField] protected ParticleSystem _shootParticle;
    [SerializeField] protected AudioSource _shootSound;

    protected float _shootInterval;
    protected bool isBought;
    protected bool isShootDelayCoroutineStart = false;

    public string Name => _name;
    public Sprite Icon => _icon;

    public float BaseShootInterval => _baseShootInterval;
    public float ShootInterval { get => _shootInterval; set => _shootInterval = value; }
    public bool IsBought => isBought;

    protected void Awake()
    {
        _shootInterval = _baseShootInterval;
    }

    protected abstract void Shoot();

    public void TryShoot()
    {
        if (isShootDelayCoroutineStart)
            return;
        else Shoot();
    }

    public void SetShootInterval(float newShootInterval)
    {
        _shootInterval = newShootInterval;
    }

    protected void StartCoroutineDelayForShoot()
    {
        StartCoroutine(DelayForShoot());
    }

    protected abstract void ApplyOptions();

    private IEnumerator DelayForShoot()
    {
        isShootDelayCoroutineStart = true;
        yield return new WaitForSeconds(_shootInterval);
        isShootDelayCoroutineStart = false;
    }
}
