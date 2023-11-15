using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class StateMachine<T> where T : System.Enum
{
    public Dictionary<T, StateBase> dictionaryState;
    private StateBase _currentState;

    public void Init()
    {
        dictionaryState = new Dictionary<T, StateBase>();
    }

    public void RegisterState(T state, StateBase stateBase)
    {
        dictionaryState.Add(state, stateBase);
    }

    public StateBase CurrentState
    {
        get { return _currentState; }
    }

    public void SwitchState(T state)
    {
        if (_currentState != null) _currentState.OnStateExit();
        _currentState = dictionaryState[state];
        _currentState.OnStateEnter();
    }

    public void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();
    }

}
