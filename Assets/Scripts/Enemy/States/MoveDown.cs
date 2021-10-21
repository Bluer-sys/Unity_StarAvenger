using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class MoveDown : State
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private State _nextState;

    private EnemyBehavior enemyBehavior;
    private Vector2 randomPosition;

    private void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
    }

    public override void Enter()
    {
        base.Enter();
        randomPosition = new Vector3(Random.Range(-6, 6), Random.Range(3, 4));
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Vector2 distance = randomPosition - (Vector2)transform.position;

        if (distance.magnitude < 0.5f)
            enemyBehavior.StateMachine.ChangeState(_nextState);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        transform.position = Vector3.Lerp(transform.position, randomPosition, _moveSpeed * Time.fixedDeltaTime);
    }
}
