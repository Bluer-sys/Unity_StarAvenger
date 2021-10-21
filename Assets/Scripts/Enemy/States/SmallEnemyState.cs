using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class SmallEnemyState : State
{
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _minMoveSpeed;
    [SerializeField] private float _maxRotationSpeed;
    [SerializeField] private float _minRotationSpeed;

    private EnemyBehavior enemyBehavior;
    private Player target;

    private float shift = 270f;
    private float randomMoveSpeed;
    private float randomRotationSpeed;

    protected void OnValidate()
    {
        if (_minMoveSpeed > _maxMoveSpeed)
            _maxMoveSpeed = _minMoveSpeed + 1;

        if (_minRotationSpeed > _maxRotationSpeed)
            _maxRotationSpeed = _minRotationSpeed + 1;
    }

    private void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();

        target = FindObjectOfType<Player>();
    }

    public override void Enter()
    {
        base.Enter();
        randomMoveSpeed = Random.Range(_minMoveSpeed, _maxMoveSpeed);
        randomRotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, randomMoveSpeed * Time.fixedDeltaTime);

        Vector2 direction = target.transform.position - transform.position;
        float angleRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angleRotation + shift);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, randomRotationSpeed * Time.fixedDeltaTime);
    }
}
