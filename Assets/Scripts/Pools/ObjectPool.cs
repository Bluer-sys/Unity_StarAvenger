using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> _pool = new List<GameObject>();

    protected virtual void Initialize(GameObject prefab, int spawnCount, Transform container)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject spawned = Instantiate(prefab, container);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    public virtual bool TryGetObjectFromPool(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }
}
