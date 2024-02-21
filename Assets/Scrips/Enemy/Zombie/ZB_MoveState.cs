using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZB_MoveState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;

    private Transform target;
    public Transform player_target;

    public float speed;
    private float cur_speed_anim;
    private float delayCheck = 0;
    private Coroutine coroutine_dt_player;
    public override void Enter()
    {
        base.Enter();
        SetTarget();
        parent.agent.isStopped = false;
        parent.agent.speed = speed;
        cur_speed_anim = 0;
        delayCheck = 0;
        if (coroutine_dt_player != null)
            parent.StopCoroutine(coroutine_dt_player);
        coroutine_dt_player = parent.StartCoroutine(LoopDetectPlayer());

        parent.running_sound.enabled = false;

    }
    public override void Update()
    {
        delayCheck += Time.deltaTime;
        base.Update();
        parent.agent.SetDestination(target.position);
        UpdateRotation();
        float speed_anim =parent.agent.velocity.magnitude / parent.agent.speed;
        cur_speed_anim = Mathf.Lerp(cur_speed_anim, speed_anim * speed, Time.deltaTime *5);
        parent.dataBinding.Speed = cur_speed_anim;


        float distanceToTarget = Vector3.Distance(parent.transform.position, target.position);
        if (distanceToTarget <= 5f)
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
        if(dir!= Vector3.zero )
        {
            Quaternion q= Quaternion.LookRotation( dir, Vector3.up );
            parent.transform.rotation = Quaternion.Slerp(parent.trans.rotation, q, Time.deltaTime * 30);
        }
    }

    IEnumerator LoopDetectPlayer()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (true)
        {
            yield return wait;
            if (parent.cur_State != parent.attackState && parent.cur_State != parent.deadState)
            {
                Collider[] cols = Physics.OverlapSphere(parent.trans_detect.position, parent.range_detect, parent.mask_player);
                int index = -1;
                if (cols.Length == 1)
                {
                    index = 0;
                }

                float distance = 50;
                for (int i = 0; i < cols.Length; i++)
                {
                    float dis = Vector3.Distance(parent.trans_detect.position, cols[i].transform.position);
                    if (dis < distance)
                    {
                        distance = dis;
                        index = i;
                    }
                }

                if (index != -1)
                    parent.GotoState(parent.attackState, cols[index].transform);
            }

        }
    }
}
