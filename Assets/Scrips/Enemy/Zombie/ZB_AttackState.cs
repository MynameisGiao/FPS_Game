using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZB_AttackState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
    private Transform player_target;
    public float speed ;
    private float cur_speed_anim;
    private float delayCheck = 0;
    private bool isAttacking;

    public override void Enter(object data)
    {
        base.Enter(data);
        player_target = (Transform)data;
        parent.agent.isStopped = false;
        parent.agent.speed = speed;
        cur_speed_anim = 0;
        delayCheck = 0;
        parent.agent.stoppingDistance = parent.range_attack;
    }

    public override void Update()
    {
        delayCheck += Time.deltaTime;
        base.Update();

        if(!isAttacking )
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

            if (delayCheck > 0.5f)
            {
                if (parent.agent.remainingDistance <= parent.range_attack + 0.1f)
                {
                    parent.dataBinding.Speed = 0;
                    if (parent.timeAttack >= parent.cf.Attack_rate)
                    {
                        parent.dataBinding.Attack = true;

                        parent.timeAttack = 0;
                    }

                }
                else
                {
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
            Debug.LogError("Attack " + parent.damage);
            MissionManager.instance.OnDamage(parent.damage);
        }

    }

    public override void OnAnimExit()
    {
        base.OnAnimExit();
        isAttacking=false;
    }
    private void UpdateRotationTarget()
    {
        Vector3 pos_tar = player_target.position;
        pos_tar.y = parent.trans.position.y;
        Vector3 dir = pos_tar - parent.trans.position;

        dir.Normalize();
        Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
        parent.trans.rotation = q;
    }
}
