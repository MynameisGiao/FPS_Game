using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionView : BaseView
{
    public AudioSource sfx;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        sfx.enabled = false ;

    }
    public void ShowHomeView()
    {
        sfx.enabled = true ;
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
        
    }
    public void ShowIngameView()
    {
        
        ViewManager.instance.SwitchView(ViewIndex.IngameView);
       
    }

    public void OnPlayMission(int id)
    {
        sfx.enabled = true;
        ConfigMissionRecord cf_mission_record=ConfigManager.instance.configMission.GetRecordBykeySearch(id);
        GameManager.instance.cur_cf_mission = cf_mission_record;
        LoadSceneManager.instance.LoadSceneByName(cf_mission_record.SceneName, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.IngameView);
        });
    }
}
