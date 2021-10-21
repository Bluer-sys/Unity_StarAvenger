using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemyCount;
    [SerializeField] private Transform _container;

    private List<Enemy> _pool = new List<Enemy>();

    public List<Enemy> Pool { get => _pool; set => _pool = value; }

    private void Awake()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            Enemy enemy = Instantiate(_enemyPrefab, _container);
            _pool.Add(enemy);

            enemy.gameObject.SetActive(false);
        }
    }

    public bool TryGetEnemyInPool(out Enemy result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }
}
