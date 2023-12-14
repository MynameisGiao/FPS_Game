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

    // Update is called once per frame
    void Update()
    {

    }
    //public override void OnShowDialog()
    //{
    //    base.OnShowDialog();
    //    //Time.timeScale = 0;
    //}
    //public override void OnHideDialog()
    //{
    //    base.OnHideDialog();
    //    //Time.timeScale = 1;
    //}
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
        DialogManager.instance.HideDialog(DialogIndex.PauseDialog);
        DialogManager.instance.ShowDialog(DialogIndex.SettingDialog);
    }
    public void OnRestart()
    {
        // Gọi hàm để load lại scene hiện tại
        DialogManager.instance.HideDialog(DialogIndex.PauseDialog);
        LoadSceneManager.instance.ReloadCurrentScene();
    }
}
