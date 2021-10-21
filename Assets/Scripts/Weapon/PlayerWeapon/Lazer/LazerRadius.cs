using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerRadius : MonoBehaviour
{
    private LazerPlayerWeapon lazerWeapon;
    private CapsuleCollider2D collider2D;
    private Vector2 standardColliderSize;

    public CapsuleCollider2D Collider2D => collider2D;
    public Vector2 StandardColliderSize => standardColliderSize;

    private void Awake()
    {
        lazerWeapon = GetComponentInParent<LazerPlayerWeapon>();
        collider2D = GetComponent<CapsuleCollider2D>();

        standardColliderSize = collider2D.size;
    }

    private void OnEnable()
    {
        lazerWeapon.EnemiesInRadius.Clear();
        lazerWeapon.CurrentLazerCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((lazerWeapon.CurrentLazerCount < lazerWeapon.LazerCount) && lazerWeapon.IsActive)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                lazerWeapon.EnemiesInRadius.Add(enemy);
                lazerWeapon.CurrentLazerCount++;

                enemy.EnemyDied += OnEnemyDied;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((lazerWeapon.CurrentLazerCount > 0) && lazerWeapon.IsActive)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                if(lazerWeapon.EnemiesInRadius.Remove(enemy))
                    lazerWeapon.CurrentLazerCount--;

                enemy.EnemyDied -= OnEnemyDied;
            }
        }
    }

    private void OnEnemyDied(Enemy enemy)
    {
        if (lazerWeapon.gameObject.activeSelf != false)
            lazerWeapon.StartCoroutineResetCollider(enemy);
    }
}
