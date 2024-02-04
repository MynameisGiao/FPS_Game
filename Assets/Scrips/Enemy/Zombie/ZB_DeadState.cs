using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZB_DeadState : FSM_State
{
    [NonSerialized]
    public Zombie parent;
}