using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameView : BaseView
{
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        Debug.LogError("Ingame View!!!");
    }
    public void ShowHomeView()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
}
