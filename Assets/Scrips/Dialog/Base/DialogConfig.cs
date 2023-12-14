using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogIndex
{
    SettingDialog=1,
    WinDialog=2,
    FailDialog=3,
    AvtDialog=4,
    QuitDialog=5,
    PauseDialog =6
}
public class DialogParam
{

}
//public class WinDialogParam: DialogParam
//{
//    public ConfigMissionRecord cf_mission;
//}

public class DialogConfig 
{
    public static DialogIndex[] dialogIndices =
    {
        DialogIndex.SettingDialog,
        DialogIndex.WinDialog,
        DialogIndex.FailDialog,
        DialogIndex.AvtDialog,
        DialogIndex.QuitDialog,
        DialogIndex.PauseDialog,
    };
}
