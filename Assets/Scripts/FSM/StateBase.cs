using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    public virtual void OnStateEnter(object o = null)
    {
        Debug.Log("OnStateEnter");
    }
    public virtual void OnStateStay(object o = null)
    {
        Debug.Log("OnStateStay");
    }
    public virtual void OnStateExit(object o = null)
    {
        Debug.Log("OnStateExit");
    }
}

