using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingDialog : BaseDialog
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
    //    Time.timeScale = 0;
    //}
    //public override void OnHideDialog()
    //{
    //    base.OnHideDialog();
    //    Time.timeScale = 1;
    //}
    public void OnClose()
    {
        DialogManager.instance.HideDialog(DialogIndex.SettingDialog);
        DialogManager.instance.ShowDialog(DialogIndex.PauseDialog);

        // DialogManager.instance.HideDialog(DialogIndex.SettingDialog);

    }
    
}
