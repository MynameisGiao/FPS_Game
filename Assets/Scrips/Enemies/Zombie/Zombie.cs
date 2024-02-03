using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EnemyControl
{
    public ZombieDataBinding dataBinding;
    //public ZB_AttackState attackState;
    //public ZB_MoveState moveState;
    //public ZB_StartState startState;
    //public ZB_DeadState deadState;

    public override void Setup(EnemyInitData enemyInitData)
    {
        base.Setup(enemyInitData);


        //attackState.parent = this;
        //moveState.parent = this;
        //startState.parent = this;
        //deadState.parent = this;

        //GotoState(startState);
    }
}
