using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinDialog : BaseDialog
{
    ConfigMissionRecord cf;
    public TMP_Text coin_lb;
    //public override void OnShowDialog()
    //{
    //    base.OnShowDialog();
    //    Time.timeScale = 0;
    //}
    //public override void OnHideDialog()
    //{
    //    base.OnHideDialog();
    //    Time.timeScale = 1;
    //}
    // Start is called before the first frame update
    public override void Setup(DialogParam param)
    {
        base.Setup(param);
        WinDialogParam dl_param = (WinDialogParam)param;
        //coin_lb.text = dl_param.cf_mission.Reward.ToString();

        // Gán giá trị cho config_gun
        cf = dl_param.cf_mission;

        Debug.LogError(" mission : " + cf.ID);
        coin_lb.text = cf.Reward.ToString();
    }

    public void OnClaim()
    {

        if (cf != null)
        {
            DataController.instance.OnGetReward(cf);
        }
        else
        {
            Debug.LogError("ConfigMissionRecord config_gun là null");
        }
        DialogManager.instance.HideDialog(dialogIndex);
        ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);

        });
    }
    

}
