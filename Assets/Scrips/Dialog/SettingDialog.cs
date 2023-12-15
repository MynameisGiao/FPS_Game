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

    private bool showPauseDialog = false;

    public override void Setup(DialogParam param)
    {
        SettingDialogParam dialogParam = param as SettingDialogParam;
        if (dialogParam != null)
        {

            showPauseDialog = dialogParam.isShowPause;
        }
        base.Setup(param);
    }
    public void OnClose()
    {
        DialogManager.instance.HideDialog(DialogIndex.SettingDialog);

        if (showPauseDialog)
        {
            DialogManager.instance.ShowDialog(DialogIndex.PauseDialog);
        }
    }

    //// Set this property when you want to control whether to show Pause Dialog
    //public void SetShowPauseDialog(bool show)
    //{
    //    showPauseDialog = show;
    //}
}
