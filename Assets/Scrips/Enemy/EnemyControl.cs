using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInitData
{
    public ConfigEnemyRecord cf;

}
public class EnemyOnDamageData
{

}

public class EnemyControl : FSM_System
{
    public Transform trans;
    public NavMeshAgent agent;
    public ConfigEnemyRecord cf;
    public int hp;
    public int damage;

    //IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(2);
    //    MissionManager.instance.EnemyDead(this);
    //}

    public virtual void Setup(EnemyInitData enemyInitData)
    {
        trans = transform;
        agent=gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.Warp(trans.position);
        cf=enemyInitData.cf; 
        hp = cf.HP;
        damage=cf.Damage;
    }
    public virtual void OnDamage(EnemyOnDamageData enemyOnDamageData)
    {

    }
    public void OnDead()
    {
        MissionManager.instance.EnemyDead(this);
    }
}
