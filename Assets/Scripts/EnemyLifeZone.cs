using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeZone : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.EnemyOutLifeZone?.Invoke(enemy);
        }
    }
}
