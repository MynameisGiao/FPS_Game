using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeView : BaseView
{
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
       // ConfigMissionRecord cf_mission_rc = ConfigManager.instance.configMission.GetRecordBykeySearch(1,2);
        Debug.LogError("Home View!!!" /*+ cf_mission_rc.SceneName*/);
    }
    public void ShowMissionView()
    {
        ViewManager.instance.SwitchView(ViewIndex.MissionView);
    }
}


