using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    #region Variables
    [SerializeField] private int _startHealth;
    [SerializeField] private int _collisionDamage;
    [SerializeField] private int _maxReward;
    [SerializeField] private int _minReward;
    [SerializeField] private float _dropItemFactor;
    
    private EffectsPool _dyingEffect;
    private ItemPool[] _dropItems;
    private Player _target;
    private int _health;
    private int _reward;

    private Coroutine takeCollisionDamage;

    public UnityAction<Enemy> EnemyDied;
    public UnityAction<Enemy> EnemyOutLifeZone;
    public UnityAction EnemyChangedDamage;

    public int Health => _health;
    public int StartHealth => _startHealth;

    #endregion

    #region CallBacks
    private void OnValidate()
    {
        if (_dropItemFactor < 0)
            _dropItemFactor = 0;

        if (_minReward > _maxReward)
            _maxReward = _minReward + 1;
    }

    private void Awake()
    {
        _target = FindObjectOfType<Player>();
        _dropItems = FindObjectsOfType<ItemPool>();
        _dyingEffect = GameObject.FindGameObjectWithTag("DyingEffect1").GetComponent<EffectsPool>();
    }

    private void OnEnable()
    {
        _health = _startHealth;
        EnemyChangedDamage?.Invoke();

        _reward = Random.Range(_minReward, _maxReward);

        EnemyDied += OnEnemyDie;
    }

    private void OnDisable()
    {
        EnemyDied -= OnEnemyDie;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.activeSelf == true)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                takeCollisionDamage = StartCoroutine(TakeDamageInCollision(player));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (takeCollisionDamage != null)
                StopCoroutine(takeCollisionDamage);
        }
    }
    #endregion

    #region Methods
    public void TakeDamage(int damage)
    {
        _health -= damage;
        EnemyChangedDamage?.Invoke();

        if (_health <= 0)
            EnemyDied?.Invoke(this);
    }

    private void OnEnemyDie(Enemy enemy)
    {
        DropTheItem();
        _target.EarnMoney(enemy._reward);

        if(_dyingEffect.TryGetEffectInPool(out GameObject effect))
        {
            effect.SetActive(true);
            effect.transform.position = transform.position;
            effect.transform.localScale = transform.localScale;
        }
    }

    private void DropTheItem()
    {
        for (int i = 0; i < _dropItems.Length; i++)
        {
            if (_dropItems[i].TryGetItemInPool(out DropItem result))
            {
                if (result.DropWithChance(_dropItemFactor))
                {
                    result.gameObject.SetActive(true);
                    result.transform.position = transform.position;
                    break;
                }
            }
        }
    }

    private IEnumerator TakeDamageInCollision(Player player)
    {
        while (true)
        {
            player.TakeDamage(_collisionDamage);
            TakeDamage(_collisionDamage * 2);

            if (_dyingEffect.TryGetEffectInPool(out GameObject effect))
            {
                effect.SetActive(true);
                effect.transform.position = transform.position;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion
}
