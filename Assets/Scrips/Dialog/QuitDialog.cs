using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitDialog : BaseDialog
{
    public AudioSource sfx;
    public void Setup()
    {
        sfx.enabled = false;
    }
    
    public void OnClose()
    {
    
        sfx.enabled=true;
        DialogManager.instance.HideDialog(DialogIndex.QuitDialog);
        DialogManager.instance.ShowDialog(DialogIndex.PauseDialog);
    }
    public void OnQuit()
    {
        sfx.enabled = true;
        DialogManager.instance.HideDialog(dialogIndex);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);
        });
    }
}
