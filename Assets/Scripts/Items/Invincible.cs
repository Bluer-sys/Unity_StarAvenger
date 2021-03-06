using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : DropItem
{
    [SerializeField] private float _duration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            PlayerBuffsApplyer playerBuffsApplyer = player.GetComponent<PlayerBuffsApplyer>();
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;

            playerBuffsApplyer.ApplyInvincibleBuff(_duration, sprite);

            gameObject.SetActive(false);
            Instantiate(_takeEffect, transform.position, Quaternion.identity);
        }
    }
}
