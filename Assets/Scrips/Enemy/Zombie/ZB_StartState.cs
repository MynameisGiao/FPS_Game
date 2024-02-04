using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ZB_StartState : FSM_State
{
    [NonSerialized]
    public Zombie parent;

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
