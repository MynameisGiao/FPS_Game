using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZB_DeadState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
    public override void Enter()
    {
        base.Enter();
        parent.dataBinding.Dead = true;
    }
    public override void Exit()
    {
        base.Exit();
        parent.OnDead();
    }
}