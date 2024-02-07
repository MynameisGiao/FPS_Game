using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;

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
        delayCheck += Time.deltaTime;
        if(!isAttacking)
        {
            if (Vector3.Distance(parent.trans.position, player_target.position) > parent.range_detect * 1.5f)
            {
                parent.GotoState(parent.moveState);
                return;
            }

            parent.agent.SetDestination(player_target.position);
            UpdateRotation();
            float speed_anim = 2; //parent.agent.velocity.magnitude / parent.agent.speed;
            cur_speed_anim = Mathf.Lerp(cur_speed_anim, speed_anim * speed, Time.deltaTime * 5);
            parent.dataBinding.Speed = cur_speed_anim;
            if (delayCheck > 0.5f)
            {
                if (parent.agent.remainingDistance <= parent.range_attack + 0.1f)
                {
                    parent.dataBinding.Speed = 0;
                    UpdateRotation();
                    if (parent.timeAttack >= parent.cf.Attack_rate)
                    {                   
                        parent.timeAttack = 0;

                        SoldierGunData soldierGunData = new SoldierGunData();
                        if (!is_InitGun)
                        {
                            is_InitGun = true;
                            soldierGunData.damage = parent.damage;
                            soldierGunData.rof = parent.attack_speed;
                            weaponBehaviour.SetupGun(soldierGunData);

                          
                        }
                        weaponBehaviour.enabled = true;
                        weaponBehaviour.isFire = true;
                        weaponBehaviour.player_target = player_target;

                        parent.dataBinding.Attack = true;
                    }

                }
                else
                {
                    weaponBehaviour.enabled = false;
                    weaponBehaviour.isFire = false;
                    parent.dataBinding.Speed = cur_speed_anim;
                }
            }
            else
            {
                parent.dataBinding.Speed = cur_speed_anim;
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
            player_target.GetComponent<CharacterControl>().OnDamage(parent.damageData);
        }

    }

    public override void OnAnimExit()
    {
        base.OnAnimExit();
     
        isAttacking = false;
    }
    private void UpdateRotation()
    {
        Vector3 dir = parent.agent.steeringTarget - parent.trans.position;
        dir.Normalize();
        if (dir != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            parent.transform.rotation = Quaternion.Slerp(parent.trans.rotation, q, Time.deltaTime * 30);
        }
    }

  
}
        
        


    

  