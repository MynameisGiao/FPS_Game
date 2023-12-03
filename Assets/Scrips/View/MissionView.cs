using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionView : BaseView
{
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        Debug.LogError("Mission View!!!");
    }
    public void ShowHomeView()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
    public void ShowIngameView()
    {
        ViewManager.instance.SwitchView(ViewIndex.IngameView);
    }

    public void OnPlayMission(int id)
    {
        ConfigMissionRecord cf_mission_record=ConfigManager.instance.configMission.GetRecordBykeySearch(id);
        GameManager.instance.cur_cf_mission_rc = cf_mission_record;
        Debug.LogError("Scene: " + cf_mission_record.SceneName);
        LoadSceneManager.instance.LoadSceneByName(cf_mission_record.SceneName, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.IngameView);
        });
    }
}
