using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public enum GameStates
    {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE
    }

    public StateMachine<GameStates> stateMachine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<GameStates>();
        stateMachine.RegisterState(GameStates.INTRO, new GMStateIntro());
        stateMachine.RegisterState(GameStates.GAMEPLAY, new StateBase());
        stateMachine.RegisterState(GameStates.PAUSE, new StateBase());
        stateMachine.RegisterState(GameStates.WIN, new StateBase());
        stateMachine.RegisterState(GameStates.LOSE, new StateBase());

        stateMachine.SwitchState(GameStates.INTRO);
    }

    public void InitGame()
    {

    }
}
