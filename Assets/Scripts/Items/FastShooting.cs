using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastShooting : DropItem
{
    [SerializeField] private float _duration;
    [SerializeField] private float _newShootInterval;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            PlayerBuffsApplyer playerBuffsApplyer = player.GetComponent<PlayerBuffsApplyer>();
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;

            playerBuffsApplyer.ApplyFastShootingBuff(_duration, _newShootInterval, sprite);

            gameObject.SetActive(false);
            Instantiate(_takeEffect, transform.position, Quaternion.identity);
        }
    }
}
