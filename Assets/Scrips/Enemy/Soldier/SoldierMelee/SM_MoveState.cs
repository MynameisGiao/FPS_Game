using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SM_MoveState : FSM_State
{
    [NonSerialized]
    public SoldierMeleeControl parent;
    private Transform target;

    public float speed;
    private float cur_speed_anim;
    private float delayCheck = 0;
    public override void Enter()
    {
        Debug.LogError("Enter move state!");
        base.Enter();
        SetTarget();
        parent.agent.isStopped = false;
        parent.agent.speed = speed;
        cur_speed_anim = 0;
        delayCheck = 0;

    }
    public override void Update()
    {
        delayCheck += Time.deltaTime;
        base.Update();
        parent.agent.SetDestination(target.position);
        UpdateRotation();
        float speed_anim = parent.agent.velocity.magnitude / parent.agent.speed;
        cur_speed_anim = Mathf.Lerp(cur_speed_anim, speed_anim * speed, Time.deltaTime * 5);
        parent.dataBinding.Speed = cur_speed_anim;


        float distanceToTarget = Vector3.Distance(parent.transform.position, target.position);
        if (distanceToTarget <= 2f)
        {

            SetTarget();
        }
    }

    private void SetTarget()
    {
        target = ConfigScene.instance.GetRandomPatrolling();
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
