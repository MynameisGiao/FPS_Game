using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class SG_DeadState : FSM_State
{
    [NonSerialized]
    public SoldierGunControl parent;
   
    public override void Enter()
    {
        base.Enter();
        parent.timeAttack = 0;
        parent.dataBinding.Dead = true;
      
    }
    public override void OnAnimMiddle()
    {
        base.OnAnimMiddle();
        parent.OnDead();

    }
}