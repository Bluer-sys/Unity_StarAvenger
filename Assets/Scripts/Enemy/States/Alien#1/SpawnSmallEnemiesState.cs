using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyBehavior))]
public class SpawnSmallEnemiesState : State
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _maxEnemyCount;
    [SerializeField] private int _minEnemyCount;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private State _nextState;

    [Inject] IEnemySpawner _spawner;
    
    private EnemyPool smallEnemyPool;
    private int enemyCount;
    private Transform[] movePoints;
    private Transform currentMovement;

    private EnemyBehavior enemyBehavior;
    private Coroutine spawnCoroutine;

    private void OnValidate()
    {
        if (_minEnemyCount > _maxEnemyCount)
            _maxEnemyCount = _minEnemyCount + 1;
    }

    private void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        movePoints = FindObjectOfType<EnemyMovePoints>().GetComponentsInChildren<Transform>();
        smallEnemyPool = GameObject.FindGameObjectWithTag("SmallEnemyPool").GetComponent<EnemyPool>();
    }

    private void OnDisable()
    {
        StopCoroutine(spawnCoroutine);
    }

    public override void Enter()
    {
        base.Enter();
        enemyCount = Random.Range(_minEnemyCount, _maxEnemyCount);
        currentMovement = movePoints[Random.Range(1, 3)];

        spawnCoroutine = StartCoroutine(SpawnSmallEnemy());
    }
    
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        Vector3 posDelay = transform.position - currentMovement.position;
        if (posDelay.magnitude < 3.0f)
        {
            currentMovement = movePoints[Random.Range(1, 3)];
        }

        transform.position = Vector3.Lerp(transform.position, currentMovement.position, _moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator SpawnSmallEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(_spawnDelay);

            if (smallEnemyPool.TryGetEnemyInPool(out Enemy enemy))
            {
                int rndPoint = Random.Range(0, _spawnPoints.Length);

                enemy.gameObject.SetActive(true);
                enemy.transform.position = _spawnPoints[rndPoint].position;

                enemy.EnemyDied += _spawner.OnUselessEnemyDied;
                enemy.EnemyOutLifeZone += _spawner.OnUselessEnemyDied;
            }
        }

        enemyBehavior.StateMachine.ChangeState(_nextState);
    }


}
