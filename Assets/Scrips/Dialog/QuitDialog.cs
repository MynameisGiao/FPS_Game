using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitDialog : BaseDialog
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
        DialogManager.instance.HideDialog(DialogIndex.QuitDialog);
        DialogManager.instance.ShowDialog(DialogIndex.PauseDialog);
    }
    public void OnQuit()
    {
        DialogManager.instance.HideDialog(dialogIndex);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);
        });
    }
}
