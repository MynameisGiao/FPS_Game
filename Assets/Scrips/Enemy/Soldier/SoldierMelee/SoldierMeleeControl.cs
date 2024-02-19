using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMeleeControl : EnemyControl
{
    public SoldierMeleeDataBinding dataBinding;
    public SM_AttackState attackState;
    public SM_StartState startState;
    public SM_MoveState moveState;
    public SM_DeadState deadState;

   
    public float timeAttack;


    public override void Setup(EnemyInitData enemyInitData)
    {
        base.Setup(enemyInitData);

        attackState.parent = this;
        startState.parent = this;
        moveState.parent = this;
        deadState.parent = this;

        GotoState(startState);

    }


    protected override void Update()
    {
        base.Update();
        timeAttack += Time.deltaTime;


    }

    public override void OnDamage(WeaponData data)
    {

        if (cur_State != deadState)
        {
            cur_hp -= data.cf.Damage;
            if (cur_hp <= 0)
            {
                GotoState(deadState);
            }
        }
        else if (cur_State == deadState)
            return;

    }
}
