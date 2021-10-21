using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected void Awake()
    {
        explosionEffect = GameObject.FindGameObjectWithTag("Explosions1").GetComponent<EffectsPool>();
    }

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);

            if (explosionEffect.TryGetEffectInPool(out GameObject effect))
            {
                effect.SetActive(true);
                effect.transform.position = transform.position;
            }

            gameObject.SetActive(false);
        }
    }
}
