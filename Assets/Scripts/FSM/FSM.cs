using System;
using System.Collections.Generic;
using UnityEngine;

public class FSM<T>
{
    private Dictionary<Type, FSMState<T>> m_StatesDictionary;
    private Dictionary<string, string> m_Datas;
    public T Owner;
    public FSMState<T> CurrentState;
    public object Data;
    

    public FSM(T owner, List<FSMState<T>> states)
    {
        Owner = owner;
        m_StatesDictionary = new Dictionary<Type, FSMState<T>>();
        m_Datas = new Dictionary<string, string>();

        foreach (var state in states)
        {
            state.OnInit(this);
            m_StatesDictionary.Add(state.GetType(), state);
        }
    }

    public void StartState<TState>() where TState : FSMState<T>
    {
        if (!m_StatesDictionary.ContainsKey(typeof(TState)))
        {
            Debug.LogError("State not found");
            return;
        }
        
        CurrentState = m_StatesDictionary[typeof(TState)];
        CurrentState.OnEnter(this);
        
    }

    public void ChangeState<TState>() where TState : FSMState<T>
    {
        

        if (!m_StatesDictionary.ContainsKey(typeof(TState)))
        {
            Debug.LogError("State not found");
            return;
        }

        CurrentState.OnLeave(this);
        CurrentState = m_StatesDictionary[typeof(TState)];
        CurrentState.OnEnter(this);
        
    }

    public void ChangeState(Type stateType)
    {
        if (!m_StatesDictionary.ContainsKey(stateType))
        {
            Debug.LogError("State not found");
            return;
        }

        CurrentState.OnLeave(this);
        CurrentState = m_StatesDictionary[stateType];
        CurrentState.OnEnter(this);
        
    }

    public void OnUpdate()
    {
        CurrentState.OnUpdate(this);
    }

    public static FSM<T> Create(T owner, List<FSMState<T>> states)
    {
        FSM<T> fsm = new FSM<T>(owner, states);
        return fsm;
    }

    public void SetData(string key, string value)
    {
        m_Datas[key] = value;
    }

    public string GetData(string key)
    {
        return m_Datas[key];
    }
    

}