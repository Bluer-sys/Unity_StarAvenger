using UnityEngine;

public abstract class State : MonoBehaviour
{
    public virtual void Enter()
    {   
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicUpdate()
    {
    }

    public virtual void Exit()
    {   
    }
}
