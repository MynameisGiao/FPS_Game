using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class SM_StartState : FSM_State
{
    [NonSerialized]
    public SoldierMeleeControl parent;
    public override void Enter()
    {
        base.Enter();
        parent.dataBinding.StartState = true;
    }
    public override void OnAnimMiddle()
    {
        base.OnAnimMiddle();
        parent.GotoState(parent.moveState);
    }
}
