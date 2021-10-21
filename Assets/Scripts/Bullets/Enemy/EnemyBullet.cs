using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private void Awake()
    {
        explosionEffect = GameObject.FindGameObjectWithTag("Explosions1").GetComponent<EffectsPool>();
    }

    private void FixedUpdate()
    {
        Move();  
    }

    protected override void Move()
    {
        transform.Translate(Vector3.up * Speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(Damage);

            if (explosionEffect.TryGetEffectInPool(out GameObject effect))
            {
                effect.SetActive(true);
                effect.transform.position = transform.position;
            }

            gameObject.SetActive(false);
        }
    }
}
