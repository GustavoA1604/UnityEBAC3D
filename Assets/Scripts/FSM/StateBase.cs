using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    public virtual void OnStateEnter(params object[] objs)
    {
    }
    public virtual void OnStateStay(params object[] objs)
    {
    }
    public virtual void OnStateExit(params object[] objs)
    {
    }
}

