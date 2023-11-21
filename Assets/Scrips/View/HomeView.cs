using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeView : BaseView
{
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        ConfigMissionRecord cf_mission = ConfigManager.instance.configMission.GetRecordBykeySearch(1,2);
        Debug.LogError("Home View!!!" + cf_mission.SceneName);
    }
    public void ShowMissionView()
    {
        ViewManager.instance.SwitchView(ViewIndex.MissionView);
    }
}

//public override void Setup(ViewParam param)
//{
//    base.Setup(param);

//    DataController.instance.CreateMissionData();
//}

//public void OnPlayeMission(int id)
//{
//    ConfigMissionRecord cf_mission = ConfigManager.instance.configMission.GetRecordBykeySearch(id);
//    GameManager.instance.cur_cf_mission = cf_mission;
//    Debug.LogError(" scene: " + cf_mission.SceneName);
//    LoadSceneManager.instance.LoadSceneByName(cf_mission.SceneName, () =>
//    {
//        ViewManager.instance.SwitchView(ViewIndex.IngameView);

//    });
//}