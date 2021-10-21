using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class MoveToTargetState : State
{
    [SerializeField] private State _stopAndShoot;

    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _minMoveSpeed;
    [SerializeField] private float _maxDuration;
    [SerializeField] private float _minDuration;

    private Player target;
    private EnemyBehavior enemyBehavior;
    private float moveSpeed;
    private float randomDuration = 0.5f;
    private float elapsedTime = 0;

    private void OnValidate()
    {
        if (_minMoveSpeed > _maxMoveSpeed)
            _maxMoveSpeed = _minMoveSpeed + 1;

        if (_minDuration > _maxDuration)
            _maxDuration = _minDuration + 1;
    }

    private void Awake()
    {
        target = FindObjectOfType<Player>();
        enemyBehavior = GetComponent<EnemyBehavior>();
    }

    public override void Enter()
    {
        base.Enter();
        moveSpeed = Random.Range(_minMoveSpeed, _maxMoveSpeed);
        randomDuration = Random.Range(_minDuration, _maxDuration);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (elapsedTime >= randomDuration)
        {
            enemyBehavior.StateMachine.ChangeState(_stopAndShoot);
            elapsedTime = 0;
        }

        elapsedTime += Time.deltaTime;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.fixedDeltaTime);
    }
}
