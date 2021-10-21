using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemyState : State
{
    [SerializeField] private float _shootDelay;
    [SerializeField] private Transform _mainShootPoint;
    [SerializeField] private Transform[] _shootPoints;
    [SerializeField] protected ParticleSystem _mainShootEffect;
    [SerializeField] protected ParticleSystem[] _shootEffects;
    [SerializeField] protected AudioSource _shootSound;

    private float elapsedTime = 0;

    private Player target;
    private EnemyBulletPool bulletPool;
    private float randomMoveSpeed;
    private Transform[] movePoints;
    private Transform currentMovement;

    private void Awake()
    {
        target = FindObjectOfType<Player>();
        bulletPool = GameObject.FindGameObjectWithTag("MediumBulletPool").GetComponent<EnemyBulletPool>();
        movePoints = FindObjectOfType<EnemyMovePoints>().GetComponentsInChildren<Transform>();
    }

    public override void Enter()
    {
        base.Enter();
        elapsedTime = _shootDelay;

        float randomValueX = UnityEngine.Random.Range(-6, 6);
        randomMoveSpeed = UnityEngine.Random.Range(1f, 2f);
        currentMovement = movePoints[UnityEngine.Random.Range(0, 4)];
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= _shootDelay)
        {
            elapsedTime = 0;
            StartCoroutine(TripleShoot());
        }

        Vector2 distance = currentMovement.position - transform.position;

        if (distance.magnitude < 0.5f)
        {
            float randomValueX = UnityEngine.Random.Range(-6, 6);
            currentMovement = movePoints[UnityEngine.Random.Range(0, 4)];

            randomMoveSpeed = UnityEngine.Random.Range(1f, 2f);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        transform.position = Vector3.MoveTowards(transform.position, currentMovement.position, randomMoveSpeed * Time.fixedDeltaTime);
    }

    private void ShootDown()
    {
        if (bulletPool.TryGetObjectFromPool(out GameObject bullet))
        {
            bullet.SetActive(true);

            bullet.transform.position = _mainShootPoint.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, 180);

            _mainShootEffect.Play();
            _shootSound.Play();
        }
    }

    private void ShootToPlayer(Transform shootPoint, ParticleSystem shootEffect)
    {
            if (bulletPool.TryGetObjectFromPool(out GameObject bullet))
            {
                bullet.SetActive(true);

                bullet.transform.position = shootPoint.position;

                Vector3 direction = target.transform.position - shootPoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                float shift = 90f;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle - shift);

                shootEffect.Play();
                _shootSound.Play();
            }
    }

    private IEnumerator TripleShoot()
    {
        for (int i = 0; i < 3; i++)
        {
            ShootDown();
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.7f);

        ShootToPlayer(_shootPoints[0], _shootEffects[0]);
        ShootToPlayer(_shootPoints[1], _shootEffects[1]);
    }
}
