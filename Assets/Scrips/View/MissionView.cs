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
}
