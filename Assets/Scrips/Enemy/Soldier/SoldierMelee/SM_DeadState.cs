using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class SM_DeadState : FSM_State
{
    [NonSerialized]
    public SoldierMeleeControl parent;
}