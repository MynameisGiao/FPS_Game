using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : EnemyControl
{
    public ZombieDataBinding dataBinding;
    public ZB_AttackState attackState;
    public ZB_StartState startState;
    public ZB_MoveState moveState;
    public ZB_DeadState deadState;

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
        if (isDead == false)
        {
            cur_hp -= data.cf.Damage;
            if (cur_hp <= 0)
            {
                isDead = true;
                GotoState(deadState);

            }
        }
    }
}
