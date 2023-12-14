using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopView : BaseView
{
   // public WeaponUIControl weaponUIControl;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
       // weaponUIControl.Setup();
    }
    public void ShowHomeView()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
    
}
