using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectsPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _effectPrefab;
    [SerializeField] private int effectCount;

    private List<GameObject> _pool = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < effectCount; i++)
        {
            GameObject effect = Instantiate(_effectPrefab, _container);
            _pool.Add(effect);
            effect.SetActive(false);
        }
    }

    public bool TryGetEffectInPool(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
}
