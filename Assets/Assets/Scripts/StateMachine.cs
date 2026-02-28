using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;

    void Start()
    {
        currentState = GetInitialState();
        if (currentState!=null)
            currentState.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
            currentState.LogicUpdate();    
    }
    void LateUpdate()
    {
        if (currentState!=null)
            currentState.PhysicsUpdate(); 
    }
    public void ChangeState(BaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public virtual BaseState GetInitialState()
    {
        return null;
    }
}
