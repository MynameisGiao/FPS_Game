using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : BYSingletonMono<MissionManager>
{
    public ConfigMissionRecord cf_mission_rc;
    void Start()
    {
        cf_mission_rc = GameManager.instance.cur_cf_mission_rc;
        Debug.LogError("init mission: " + cf_mission_rc.Name);   
    }

    void Update()
    {
        
    }
}

