using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

public class MissionManager : BYSingletonMono<MissionManager>
{
    public ConfigMissionRecord cf_mission;
    private List<int> waves;
    private int index_wave = -1;
    private int number_enemy_dead;
    private int total_enemy;
    private int count_enemy_create;
    public event Action<int, int> OnWaveChange;
    //public UnityEvent<int, int> OnWaveChange;
    // private int hp = 20;
    // private int max_hp = 20;
    public UnityEvent<int, int> OnBaseHpChange;
    private bool isEndMission = false;

    IEnumerator Start()
    {
        cf_mission = GameManager.instance.cur_cf_mission;
        waves = cf_mission.Waves;
        yield return new WaitForSeconds(5);
        StartCoroutine("CreateNewWave");
    }
   
    IEnumerator CreateNewWave()
    {
        index_wave++;
        if (index_wave >= waves.Count)
        {
            // mission complete
            Debug.LogError("Mission Complete");
            WinDialogParam param = new WinDialogParam();
            param.cf_mission = cf_mission;
            DialogManager.instance.ShowDialog(DialogIndex.WinDialog,param);


            //OnWaveChange.RemoveAllListeners();
            //OnBaseHpChange.RemoveAllListeners();
            //Debug.LogError(" mission complete ");
            //BYPoolManager.instance.GetPool("HPHub").DeSpawnAll();
            
            
            //DialogManager.instance.ShowDialog(DialogIndex.WinDialog, param);
        }
        else
        {
            Debug.LogError("Wave: "+ index_wave);
            ConfigWaveRecord cf_wave = ConfigManager.instance.configWave.GetRecordBykeySearch(waves[index_wave]);
            total_enemy = cf_wave.Enemies.Count;
            count_enemy_create = 0;
            number_enemy_dead = 0;
            OnWaveChange?.Invoke(index_wave + 1, waves.Count);

            yield return new WaitForSeconds(cf_wave.Time_Delay);
            for (int i = 0; i < cf_wave.Enemies.Count; i++)
            {
                StartCoroutine(CreateEnemy(cf_wave.Time_Spawns[i], cf_wave.Enemies[i]));
            }

        }
    }
    IEnumerator CreateEnemy(int delay, int id)
    {
        yield return new WaitForSeconds(delay);
        Debug.LogError("Enemy : " + id);

        count_enemy_create++;
        ConfigEnemyRecord cf_enemy = ConfigManager.instance.configEnemy.GetRecordBykeySearch(id);
        GameObject e_obj = Instantiate(Resources.Load("Enemy/" + cf_enemy.Prefab, typeof(GameObject))) as GameObject;
        // create enemy 
        //count_enemy_create++;
        //ConfigEnemyRecord cf_enemy = ConfigManager.instance.configEnemy.GetRecordBykeySearch(id);
        //GameObject e_obj = Instantiate(Resources.Load("Enemy/" + cf_enemy.Prefab, typeof(GameObject))) as GameObject;
        //Transform pos_trans = ConfigScene.instance.GetEnemySpawnPoint();
        //e_obj.transform.position = pos_trans.position;
        //e_obj.transform.forward = pos_trans.forward;
        //EnemyControl enemyControl = e_obj.GetComponent<EnemyControl>();
        //enemyControl.Setup(new EnemyInitData { config_gun = cf_enemy });
    }
    public void EnemyDead(EnemyControl e)
    {
        number_enemy_dead++;
        if (count_enemy_create >= total_enemy)
        {
            if (number_enemy_dead >= total_enemy)
            {
                StartCoroutine("CreateNewWave");
            }
        }
    }
    //// Update is called once per frame
    //void Update()
    //{

    //}
    //public void OnDamage(DamageData damageData)
    //{
    //    // Debug.LogError(" enemy attack base : " + damageData.damage);
    //    hp -= damageData.damage;
    //    if (hp <= 0)
    //    {
    //        hp = 0;
    //        //
    //        if (!isEndMission)
    //        {
    //            OnWaveChange.RemoveAllListeners();
    //            OnBaseHpChange.RemoveAllListeners();
    //            BYPoolManager.instance.GetPool("HPHub").DeSpawnAll();
    //            DialogManager.instance.ShowDialog(DialogIndex.FailDialog);
    //            isEndMission = true;
    //        }
    //    }
    //    OnBaseHpChange?.Invoke(hp, max_hp);
    //}
    //public void OnCreateUnit(UnitData unitData, ConfigUnitRecord cf_unit, Vector3 posCreate)
    //{

    //    GameObject unit = Instantiate(Resources.Load("Unit/" + cf_unit.Prefab, typeof(GameObject))) as GameObject;
    //    unit.transform.position = posCreate;
    //    unit.GetComponent<UnitControl>().Setup(new UnitInitData { configUnit = cf_unit, unitData = unitData });
    //}


}