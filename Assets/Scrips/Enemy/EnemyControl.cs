using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInitData
{
    public ConfigEnemyRecord cf;

}
public class DamageData
{
    public int damage;
}

public class EnemyControl : FSM_System
{
   
    public Transform trans;
    public Transform trans_detect;
    public float range_detect;
    public float range_attack;
    public NavMeshAgent agent;
    public ConfigEnemyRecord cf;
    public int hp;
    public int cur_hp;
    public int damage;
   
    public float attack_speed;
    public DamageData damageData = new DamageData();
    public LayerMask mask_player;
    public Action<int, int, int> OnHpChange;

    

    public virtual void Setup(EnemyInitData enemyInitData)
    {
        trans = transform;
        agent=gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.Warp(trans.position);
        cf=enemyInitData.cf; 
        hp = cf.HP;
        cur_hp = hp;
        attack_speed = cf.Attack_rate;
        damageData.damage=cf.Damage;
        damage=cf.Damage; // lấy damage ra dùng cho SoldierGun
    
    }
    public virtual void OnDamage(WeaponData data)
    {
        
    }
    public virtual void OnDead()
    {
        MissionManager.instance.EnemyDead(this);
        Destroy(gameObject);
        MissionManager.instance.PlusHP();
    }

   
}

