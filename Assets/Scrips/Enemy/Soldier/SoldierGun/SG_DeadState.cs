using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class SG_DeadState : FSM_State
{
    [NonSerialized]
    public SoldierGunControl parent;
}