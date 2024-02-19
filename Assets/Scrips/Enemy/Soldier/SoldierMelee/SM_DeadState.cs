using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class SM_DeadState : FSM_State
{
    [NonSerialized]
    public SoldierMeleeControl parent;
    public override void Enter()
    {
        base.Enter();
        parent.dataBinding.Dead = true;

    }
    public override void OnAnimMiddle()
    {
        base.OnAnimMiddle();
        Debug.LogError("Exit");
        parent.OnDead();

    }
}