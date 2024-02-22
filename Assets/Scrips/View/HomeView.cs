using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeView : BaseView
{
    public AudioSource sfx;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        sfx.enabled = false;
      
      
    }
    public void ShowMissionView()
    {
        sfx.enabled = true;

        ViewManager.instance.SwitchView(ViewIndex.MissionView);
    }
    public void ShowShopView()
    {
        sfx.enabled = true;

        ViewManager.instance.SwitchView(ViewIndex.ShopView);
    }
    public void OnSetting()
    {
       
        DialogManager.instance.ShowDialog(DialogIndex.SettingDialog,new SettingDialogParam { isShowPause=false});

    }
    public void ShowInputName()
    {
       
        if (ViewManager.instance.cur_view.viewIndex == ViewIndex.HomeView)
        {
            DialogManager.instance.ShowDialog(DialogIndex.InputNameDialog);
        }
       
    }
}


