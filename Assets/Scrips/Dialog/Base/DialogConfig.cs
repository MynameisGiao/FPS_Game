using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogIndex
{
    SettingDialog=1,
    WinDialog=2,
    FailDialog=3,
    WeaponInfoDialog=4,
    QuitDialog=5,
    PauseDialog =6,
    DeckEquipDialog =7, 
    InputNameDialog=8
}
public class DialogParam
{

}
public class WinDialogParam : DialogParam
{
    public ConfigMissionRecord cf_mission;
}
public class SettingDialogParam : DialogParam
{
    public bool isShowPause;
}
public class WeaponInfoDialogParam : DialogParam
{
    public ConfigGunRecord cf_gun;
}
public class DeckEquipDialogParam : DialogParam
{
    public GunData gunData;
}

public class DialogConfig 
{
    public static DialogIndex[] dialogIndices =
    {
        DialogIndex.SettingDialog,
        DialogIndex.WinDialog,
        DialogIndex.FailDialog,
        DialogIndex.WeaponInfoDialog,
        DialogIndex.QuitDialog,
        DialogIndex.PauseDialog,
        DialogIndex.DeckEquipDialog,
        DialogIndex.InputNameDialog,
    };
}
