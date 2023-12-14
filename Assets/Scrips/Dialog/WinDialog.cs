using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDialog : BaseDialog
{
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
    // Start is called before the first frame update
    public override void Setup(DialogParam param)
    {
        base.Setup(param);
       // WinDialogParam dl_param = (WinDialogParam)param;

       // Debug.LogError(" mission : " + dl_param.cf_mission.ID);
    }

    public void OnClaim()
    {
        DialogManager.instance.HideDialog(dialogIndex);
        ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);

        });
    }
    public void OnClaim2X()
    {
        //
        DialogManager.instance.HideDialog(dialogIndex);
        ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);

        });
    }

}
