using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Variables

    [SerializeField] private int _maxHealth;
    [SerializeField] private ParticleSystem _shieldEffect;

    private EffectsPool dyingEffectPool;

    private int _health;
    private int _money;
    private bool isGodMod = false;

    public UnityAction<int, int> HealthChanged;
    public UnityAction<int, int> MoneyChanged;
    public UnityAction<int> PlayerDied;

    public int Money => _money;
    #endregion

    #region Callbacks

    private void Awake()
    {
        ApplyOptions();

        dyingEffectPool = GameObject.FindGameObjectWithTag("DyingEffect1").GetComponent<EffectsPool>();
    }

    private void Start()
    {
        _health = _maxHealth;
        Debug.Log(_health);

        HealthChanged?.Invoke(_health, _maxHealth);
        MoneyChanged?.Invoke(0, _money);
    }

    #endregion

    #region Methods

    public void TakeDamage(int damage)
    {
        if (!isGodMod)
        {
            _health -= damage;
            HealthChanged?.Invoke(_health, _maxHealth);

            if (_health <= 0)
                Die();
        }
    }

    public void EarnMoney(int money)
    {
        MoneyChanged?.Invoke(_money, _money + money);
        _money += money;
    }

    public void TryHealing(int healthPoints)
    {
        if (_health + healthPoints <= _maxHealth)
        {
            _health += healthPoints;
        }
        else _health = _maxHealth;

        HealthChanged?.Invoke(_health, _maxHealth);
    }

    private void Die()
    {
        gameObject.SetActive(false);
        if(dyingEffectPool.TryGetEffectInPool(out GameObject effect))
        {
            effect.SetActive(true);
            effect.transform.position = transform.position;
        }
        PlayerDied?.Invoke(_money);
    }

    private void ApplyOptions()
    {
        _money = GameSceneController.PlayerData.money;

        switch (GameSceneController.PlayerData.shipModel)
        {
            case 1:
                _maxHealth = 50;
                break;
            case 2:
                _maxHealth = 100;
                break;
            case 3:
                _maxHealth = 150;
                break;
            case 4:
                _maxHealth = 200;
                break;
            case 5:
                _maxHealth = 250;
                break;
            case 6:
                _maxHealth = 300;
                break;
            default:
                break;
        }
    }

    public void SwitchInvincible(bool isInvincibleVariable)
    {
        if (isInvincibleVariable)
        {
            isGodMod = true;
            _shieldEffect.Play();
        }
        else
        {
            isGodMod = false;
            _shieldEffect.Stop();
        }
    }

    #endregion
}
