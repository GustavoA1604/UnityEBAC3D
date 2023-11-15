using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class FSMExample : MonoBehaviour
{
    public enum ExampleEnum
    {
        STATE_ONE,
        STATE_TWO,
        STATE_THREE
    }

    public StateMachine<ExampleEnum> stateMachine;

    void Start()
    {
        stateMachine = new StateMachine<ExampleEnum>();
        stateMachine.Init();
        stateMachine.RegisterState(ExampleEnum.STATE_ONE, new StateBase());
        stateMachine.RegisterState(ExampleEnum.STATE_TWO, new StateBase());
    }
}
