using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T> where T : IFSMState
{
    public Dictionary<System.Type, T> StateTable {get; protected set;}
    public T currentState;

    public FSM()
    {
        StateTable = new Dictionary<System.Type, T>();
    }

    public void AddState(System.Type type, T state)
    {
        StateTable.Add(type, state);
    }

    public void SwitchOn(System.Type type)
    {
        currentState = StateTable[type];
        currentState.Enter();
    }

    public void ChangeState(System.Type type)
    {
        currentState.Exit();
        currentState = StateTable[type];
        currentState.Enter();
    }

    public void Update()
    {
        currentState.Update();
    }

    
}
