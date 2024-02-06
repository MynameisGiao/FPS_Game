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

    public Transform player_target;
    public float timeAttack;


    public override void Setup(EnemyInitData enemyInitData)
    {
        base.Setup(enemyInitData);

        attackState.parent = this;
        startState.parent = this;
        moveState.parent = this;
        deadState.parent = this;

        GotoState(startState);
        StartCoroutine("LoopDetectPlayer");

    }

    IEnumerator LoopDetectPlayer()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (true)
        {
            yield return wait;
            if (cur_State != attackState && cur_State != deadState)
            {
                Collider[] cols = Physics.OverlapSphere(trans_detect.position, range_detect, mask_player);
                int index = -1;
                if (cols.Length == 1)
                {
                    index = 0;
                }

                float distance = 50;
                for (int i = 0; i < cols.Length; i++)
                {
                    float dis = Vector3.Distance(trans_detect.position, cols[i].transform.position);
                    if (dis < distance)
                    {
                        distance = dis;
                        index = i;
                    }
                }

                if (index != -1)
                    GotoState(attackState, cols[index].transform);
            }

        }
    }
    protected override void Update()
    {
        base.Update();
        timeAttack += Time.deltaTime;
    }
}
