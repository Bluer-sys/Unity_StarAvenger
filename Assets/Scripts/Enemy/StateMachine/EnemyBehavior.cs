using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private State _initialState;

    protected EnemyStateMachine EnemyStateMachine;
    
    public EnemyStateMachine StateMachine => EnemyStateMachine;

    private void Awake()
    {
        EnemyStateMachine = new EnemyStateMachine();
        EnemyStateMachine.Initialize(_initialState);
    }

    private void OnEnable()
    {
        EnemyStateMachine.ChangeState(_initialState);
    }

    private void Update()
    {
        EnemyStateMachine.CurrentState.HandleInput();
        EnemyStateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        EnemyStateMachine.CurrentState.PhysicUpdate();
    }
}
