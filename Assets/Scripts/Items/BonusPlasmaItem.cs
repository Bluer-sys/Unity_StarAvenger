using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPlasmaItem : DropItem
{
    [SerializeField] private int _minPlasma;
    [SerializeField] private int _maxPlasma;

    private int bonusPlasma;

    private void OnValidate()
    {
        if (_minPlasma > _maxPlasma)
            _maxPlasma = _minPlasma + 1;
    }

    private void OnEnable()
    {
        bonusPlasma = Random.Range(_minPlasma, _maxPlasma);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            gameObject.SetActive(false);
            player.EarnMoney(bonusPlasma);
            Instantiate(_takeEffect, transform.position, Quaternion.identity);
        }
    }
}
