using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinDialog : BaseDialog
{
    ConfigMissionRecord cf;
    public TMP_Text coin_lb;
    public override void OnShowDialog()
    {
        base.OnShowDialog();
        Time.timeScale = 0;
    }
    public override void OnHideDialog()
    {
        base.OnHideDialog();
        Time.timeScale = 1;
    }
  
    public override void Setup(DialogParam param)
    {
        base.Setup(param);
        WinDialogParam dl_param = (WinDialogParam)param;
        cf = dl_param.cf_mission;
        coin_lb.text = cf.Reward.ToString();
    }

    public void OnClaim()
    {

        DataController.instance.OnGetReward(cf);
        DialogManager.instance.HideDialog(dialogIndex);
        ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);

        });
    }
    

}
