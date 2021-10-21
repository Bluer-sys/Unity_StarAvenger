using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class StopAndShootState : State
{
    [SerializeField] private EnemyBehavior _enemyBehavior;
    [SerializeField] private State _moveToTarget;

    [SerializeField] protected Transform _shootPoint;
    [SerializeField] protected float _maxShootDelay;
    [SerializeField] protected float _minShootDelay;
    [SerializeField] protected ParticleSystem _shootEffect;
    [SerializeField] protected AudioSource _shootSound;

    protected EnemyBulletPool bulletPool;
    protected Player target;
    protected float randomDuration;
    protected float randomShootDelay;
    private float elapsedTimeForStateDuration;
    protected float elapsedTimeForShootDelay;

    protected virtual void OnValidate()
    {
        if (_minShootDelay > _maxShootDelay)
            _maxShootDelay = _minShootDelay + 1;
    }

    private void Awake()
    {
        target = FindObjectOfType<Player>();
        bulletPool = GameObject.FindGameObjectWithTag("SimpleBulletPool").GetComponent<EnemyBulletPool>();
    }

    public override void Enter()
    {
        base.Enter();
        randomDuration = Random.Range(3.0f, 4.5f);
        randomShootDelay = Random.Range(_minShootDelay, _maxShootDelay);

        elapsedTimeForStateDuration = 0;
        elapsedTimeForShootDelay = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (elapsedTimeForStateDuration >= randomDuration)
        {
            _enemyBehavior.StateMachine.ChangeState(_moveToTarget);
            elapsedTimeForStateDuration = 0;
        }

        elapsedTimeForStateDuration += Time.deltaTime;
        elapsedTimeForShootDelay += Time.deltaTime;
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        Shoot();
    }

    protected virtual void Shoot()
    {
        if (elapsedTimeForShootDelay >= randomShootDelay)
        {
            if (bulletPool.TryGetObjectFromPool(out GameObject bullet))
            {
                bullet.SetActive(true);

                bullet.transform.position = _shootPoint.position;

                Vector3 direction = target.transform.position - _shootPoint.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                float shift = 90f;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle - shift);

                _shootEffect.Play();
                _shootSound.Play();
            }

            elapsedTimeForShootDelay = 0;
        }
    }
}
