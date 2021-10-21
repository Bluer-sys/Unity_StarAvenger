using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien1ShootState : State
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootInterval;
    [SerializeField] private float _translationInterval;
    [SerializeField] private float _stateDuration;
    [SerializeField] private ParticleSystem _shootEffect;
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private float _translationSpeed;
    [SerializeField] private State _nextState;

    private Player target;
    private EnemyBulletPool bulletPool;
    private float elapsedTimeShoot;
    private float elapsedTimeMove;
    private Transform[] movePoints;
    private Transform translation;

    private EnemyBehavior enemyBehavior;

    private Coroutine stateDurationCoroutine;

    private void Start()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        target = FindObjectOfType<Player>();
        bulletPool = GameObject.FindGameObjectWithTag("Alien#1BulletPool").GetComponent<EnemyBulletPool>();
        movePoints = FindObjectOfType<EnemyMovePoints>().GetComponentsInChildren<Transform>();
    }

    public override void Enter()
    {
        base.Enter();

        translation = movePoints[Random.Range(1, 3)];

        stateDurationCoroutine = StartCoroutine(StateDuration());
    }

    public override void Exit()
    {
        base.Exit();

        StopCoroutine(stateDurationCoroutine);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        elapsedTimeShoot += Time.deltaTime;
        elapsedTimeMove += Time.deltaTime;

        if(elapsedTimeShoot >= _shootInterval)
        {
            elapsedTimeShoot = 0;
            Shoot();
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        transform.position = Vector3.MoveTowards(transform.position, translation.position, _translationSpeed * Time.fixedDeltaTime);

        if (elapsedTimeMove >= _translationInterval)
        {
            elapsedTimeMove = 0;
            translation = movePoints[Random.Range(1, 3)];
        }
    }

    private void Shoot()
    {
        if(bulletPool.TryGetObjectFromPool(out GameObject bullet))
        {
            bullet.SetActive(true);
            bullet.transform.position = _shootPoint.position;

            Vector3 rotation = target.transform.position - _shootPoint.position;
            float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            float shift = -90f;

            bullet.transform.rotation = Quaternion.Euler(0, 0, angle + shift);

            _shootEffect.Play();
            _shootSound.Play();
        }
    }

    private IEnumerator StateDuration()
    {
        yield return new WaitForSeconds(_stateDuration);

        enemyBehavior.StateMachine.ChangeState(_nextState);
    }
}
