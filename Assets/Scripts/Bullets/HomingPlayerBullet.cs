using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingPlayerBullet : PlayerBullet
{
    private Coroutine autoDestroyCoroutine;
    private Quaternion startRotation;

    private void Awake()
    {
        base.Awake();

        startRotation = transform.rotation;
    }

    private void OnEnable()
    {
        autoDestroyCoroutine = StartCoroutine(AutoDestroy());
    }

    private void OnDisable()
    {
        if (autoDestroyCoroutine != null)
            StopCoroutine(autoDestroyCoroutine);

        transform.rotation = startRotation;
    }

    protected override void Move()
    {
        base.Move();
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}
