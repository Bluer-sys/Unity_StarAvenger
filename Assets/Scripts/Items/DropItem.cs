using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class DropItem : BonusItem
{
    [SerializeField] protected ParticleSystem _takeEffect;
    [SerializeField] protected float _fallSpeed;
    [SerializeField] protected float _dropChance;

    private void OnEnable()
    {
        StartCoroutine(AutoDisable());
    }

    private void FixedUpdate()
    {
        FallDown();
    }

    private void FallDown()
    {
        transform.Translate(Vector3.down * _fallSpeed);
    }

    public bool DropWithChance(float factor)
    {
        float randomValue = Random.Range(0, 1.0f) / factor;

        if (randomValue > _dropChance)
            return false;

        return true;
    }

    private IEnumerator AutoDisable()
    {
        yield return new WaitForSeconds(4.0f);
        gameObject.SetActive(false);
    }
}
