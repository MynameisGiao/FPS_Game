using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;
using Unity.IO.LowLevel.Unsafe;

[Serializable]
public class SG_AttackState : FSM_State
{
    [NonSerialized]
    public SoldierGunControl parent;
    private Transform player_target; 
    public float speed;
    private float cur_speed_anim;
    private float delayCheck = 0;
    private bool isAttacking;

    public SG_Weapon_Behaviour weaponBehaviour;
    private bool is_InitGun = false;

    public override void Enter(object data)
    {
        base.Enter(data);
        player_target = (Transform)data;

        parent.agent.speed = speed;
        cur_speed_anim = 0;
        delayCheck = 0;
        parent.agent.stoppingDistance = parent.range_attack;

       
    }
    public override void Update()
    {
        base.Update();
        if (parent.isDead == false && parent.cur_State != parent.deadState)
        {
            delayCheck += Time.deltaTime;
            if (!isAttacking && parent.isDead == false)
            {
                if (Vector3.Distance(parent.trans.position, player_target.position) > parent.range_detect * 1.5f)
                {
                    parent.GotoState(parent.moveState);
                    return;
                }

                parent.agent.SetDestination(player_target.position);
                UpdateRotationTarget();
                float speed_anim = 2; //parent.agent.velocity.magnitude / parent.agent.speed;
                cur_speed_anim = Mathf.Lerp(cur_speed_anim, speed_anim * speed, Time.deltaTime * 5);
                parent.dataBinding.Speed = cur_speed_anim;
                parent.running_sound.enabled = true;
                if (delayCheck > 0.5f && parent.isDead == false)
                {
                    if (parent.agent.remainingDistance <= parent.range_attack + 0.1f && parent.isDead == false)
                    {
                        parent.dataBinding.Speed = 0;
                        parent.running_sound.enabled = false;
                        UpdateRotationTarget();
                        if (parent.timeAttack >= parent.cf.Attack_rate && parent.isDead == false)
                        {
                            parent.timeAttack = 0;

                            SoldierGunData soldierGunData = new SoldierGunData();
                            if (!is_InitGun && parent.isDead == false)
                            {
                                is_InitGun = true;
                                soldierGunData.sg_Control = parent;
                                soldierGunData.damage = parent.damage;
                                soldierGunData.rof = parent.attack_speed;
                                weaponBehaviour.SetupGun(soldierGunData);


                            }
                            weaponBehaviour.enabled = true;
                            weaponBehaviour.isFire = true;
                            parent.fire.enabled = true;
                            weaponBehaviour.player_target = player_target;


                        }

                    }
                    else
                    {
                        weaponBehaviour.enabled = false;
                        weaponBehaviour.isFire = false;
                        parent.fire.enabled = false;
                        parent.dataBinding.Speed = cur_speed_anim;
                        parent.running_sound.enabled = true;
                    }
                }
                else
                {
                    parent.dataBinding.Speed = cur_speed_anim;
                    parent.running_sound.enabled = true;
                }
            }
        }
        

    }
    public override void OnAnimEnter()
    {
        base.OnAnimEnter();
        isAttacking = true;
    }
    public override void OnAnimMiddle()
    {
        base.OnAnimMiddle();
        if (Vector3.Distance(parent.trans.position, player_target.position) <= parent.range_attack + 0.1f)
        {
            MissionManager.instance.OnDamage(parent.damage);
        }

    }

    public override void OnAnimExit()
    {
        base.OnAnimExit();
     
        isAttacking = false;
    }
    private void UpdateRotationTarget()
    {
        Vector3 pos_tar = player_target.position;
        pos_tar.y= parent.trans.position.y;
        Vector3 dir= pos_tar -parent.trans.position;

        dir.Normalize();
        Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
        parent.trans.rotation = q;
    }
    public override void Exit()
    {
        base.Exit();
        parent.agent.stoppingDistance = 0;
        parent.agent.isStopped = true;
        parent.dataBinding.Speed = 0;
        delayCheck = 0;
    }
  
}
        
        


    

  