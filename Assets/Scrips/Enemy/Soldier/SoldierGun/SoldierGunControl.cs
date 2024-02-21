using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierGunControl : EnemyControl
{
    public SoldierGunDataBinding dataBinding;
    public SG_AttackState attackState;
    public SG_StartState startState;
    public SG_MoveState moveState;
    public SG_DeadState deadState;

    
    public float timeAttack;
    public AudioSource fire;
   

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
        if (isDead==true)
        {
            timeAttack = 0;
        } else 
        { 
            timeAttack += Time.deltaTime;
        }
        
        
    }

    public override void OnDamage(WeaponData data)
    {
        if (isDead == false)
        {
            cur_hp -= data.cf.Damage;
            if (cur_hp <= 0)
            {
                isDead = true;
                timeAttack = 0;
                cur_hp = 0;
                attackState.weaponBehaviour.enabled = false;
                attackState.weaponBehaviour.isFire = false;
                fire.enabled = false;

                GotoState(deadState);
            }
        }
    }


}
