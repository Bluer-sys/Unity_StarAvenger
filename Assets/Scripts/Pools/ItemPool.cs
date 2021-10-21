using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField] private DropItem _itemPrefab;
    [SerializeField] private int _itemCount;
    [SerializeField] private Transform _container;

    private List<DropItem> _pool = new List<DropItem>();

    private void Start()
    {
        for (int i = 0; i < _itemCount; i++)
        {
            DropItem item = Instantiate(_itemPrefab, _container);
            _pool.Add(item);
            item.gameObject.SetActive(false);
        }
    }

    public bool TryGetItemInPool(out DropItem result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result != null;
    }
}
