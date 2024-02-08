using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailDialog : BaseDialog
{
   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnBack()
    {
        DialogManager.instance.HideDialog(DialogIndex.FailDialog);
        ViewManager.instance.SwitchView(ViewIndex.EmptyView);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.MissionView);
        });
    }

}
