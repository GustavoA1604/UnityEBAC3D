using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class StateMachine : MonoBehaviour
{
    public enum States
    {
        NONE
    }

    public Dictionary<States, StateBase> dictionaryState;

    private StateBase _currentState;
    public float timeToStartGame = 1f;

    private void Awake()
    {
        dictionaryState = new Dictionary<States, StateBase>();
        dictionaryState.Add(States.NONE, new StateBase());

        SwitchState(States.NONE);
        Invoke(nameof(StartGame), timeToStartGame);
    }

    [Button]
    private void StartGame()
    {
        SwitchState(States.NONE);
    }

    private void SwitchState(States state)
    {
        if (_currentState != null) _currentState.OnStateExit();

        _currentState = dictionaryState[state];

        _currentState.OnStateEnter();
    }

    private void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();
    }

}
