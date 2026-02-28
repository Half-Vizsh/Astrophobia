using System;
using Unity.VisualScripting;
using UnityEngine;

public class BaseState
{
    public String name;
    protected StateMachine stateMachine;
    public BaseState(String name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter(){}
    public virtual void LogicUpdate(){}
    public virtual void PhysicsUpdate(){}
    public virtual void Exit(){}
    public virtual void Attack(){}
    //P.s you can use abstract (no implementation, must be overriden later) or virtual (can be optionally overriden)
}
