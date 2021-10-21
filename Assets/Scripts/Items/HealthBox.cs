using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : DropItem
{
    [SerializeField] private int _minHealing;
    [SerializeField] private int _maxHealing;

    private int healing;

    private void OnValidate()
    {
        if (_minHealing > _maxHealing)
            _maxHealing = _minHealing + 1;
    }

    private void OnEnable()
    {
        healing = Random.Range(_minHealing, _maxHealing);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            gameObject.SetActive(false);
            player.TryHealing(healing);
            Instantiate(_takeEffect, transform.position, Quaternion.identity);
        }
    }
}