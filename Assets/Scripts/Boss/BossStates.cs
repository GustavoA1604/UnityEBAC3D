using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateBase : StateBase
{
    protected BossBase boss;

    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        boss = (BossBase)objs[0];
    }
}

public class BossStateInit : BossStateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        boss.StartInitAnimation();
    }
}

public class BossStateWalk : BossStateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        boss.GotoRandomPoint(OnArrive);
    }

    private void OnArrive()
    {
        boss.SwitchState(BossAction.ATTACK);
    }
}

public class BossStateAttack : BossStateBase
{
    public override void OnStateEnter(params object[] objs)
    {
        base.OnStateEnter(objs);
        boss.StartAttack(EndCallback);
    }

    private void EndCallback()
    {
        boss.SwitchState(BossAction.WALK);
    }
}
