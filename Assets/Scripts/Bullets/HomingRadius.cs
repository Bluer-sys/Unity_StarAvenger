using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HomingRadius : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private HomingPlayerBullet homingBullet;
    private Coroutine rotationCoroutine;
    private Collider2D collider2D;

    private void Awake()
    {
        homingBullet = GetComponentInParent<HomingPlayerBullet>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        collider2D.enabled = true;
    }

    private void OnDisable()
    {
        if(rotationCoroutine != null)
            StopCoroutine(rotationCoroutine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Vector3 direction = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if(homingBullet.gameObject.activeSelf)
                rotationCoroutine = StartCoroutine(RotationLerp(angle));

            collider2D.enabled = false;
        }
    }

    private IEnumerator RotationLerp(float angle)
    {
        float step = -90f;
        Quaternion quaternion = Quaternion.Euler(0, 0, angle + step);

        while (homingBullet.transform.rotation != quaternion)
        {
            homingBullet.transform.rotation = Quaternion.RotateTowards(homingBullet.transform.rotation, quaternion, _rotationSpeed);
            yield return new WaitForFixedUpdate();
        }
    }
}
