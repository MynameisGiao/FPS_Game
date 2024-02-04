using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ZB_MoveState : FSM_State
{
    [NonSerialized]
    public Zombie parent;

    private Transform target;
  
    public override void Enter()
    {
        base.Enter();
        SetTarget();
       
    }
    public override void Update()
    {
        base.Update();
        parent.agent.SetDestination(target.position);

        float distanceToTarget = Vector3.Distance(parent.transform.position, target.position);
        //Debug.Log("Distance to target: " + distanceToTarget); =>  distance < 1f

        if ((distanceToTarget) <= 1f)
        {
            Debug.LogError("SET NEW TARGET!!!!!!!!!!!!");
            SetTarget();
        }
    }

    public void SetTarget()
    {
        target = ConfigScene.instance.GetRandomPatrolling();
    }
   
}
