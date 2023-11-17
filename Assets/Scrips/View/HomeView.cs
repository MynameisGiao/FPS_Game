using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeView : BaseView
{
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        Debug.LogError("Home View!!!");
    }
    public void ShowIngameView()
    {
        ViewManager.instance.SwitchView(ViewIndex.IngameView);
    }
}
