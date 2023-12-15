using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDialog : BaseDialog
{
   

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

    }
 
    public void OnClose()
    {
        DialogManager.instance.HideDialog(DialogIndex.PauseDialog);

    }
    public void OnQuitDialog()
    {
        DialogManager.instance.HideDialog(DialogIndex.PauseDialog);
        DialogManager.instance.ShowDialog(DialogIndex.QuitDialog);
    }
    public void OnSettingDialog()
    {
        DialogManager.instance.ShowDialog(DialogIndex.SettingDialog, new SettingDialogParam { isShowPause = true });
    }
    public void OnRestart()
    {
        // Gọi hàm để load lại scene hiện tại
        DialogManager.instance.HideDialog(DialogIndex.PauseDialog);
        LoadSceneManager.instance.ReloadCurrentScene();
    }
}
