using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDialog : BaseDialog
{
    // Start is called before the first frame update
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
    public void OnClose()
    {
        DialogManager.instance.HideDialog(DialogIndex.PauseDialog);
    }
    public void OnQuit()
    {
        DialogManager.instance.HideDialog(dialogIndex);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
         //   ViewManager.instance.SwitchView(ViewIndex.HomeView);

        });
    }
}
