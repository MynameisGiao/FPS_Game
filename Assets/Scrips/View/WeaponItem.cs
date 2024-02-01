using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    public Image weapon_Icon;
    public Text name_lb;  
    public ShopView shopView;
    public GameObject item_select;
    private ConfigGunRecord cf;
    public void Setup(ConfigGunRecord cf)
    {
        this.cf = cf;
        name_lb.text=cf.Name.ToString();
        weapon_Icon.overrideSprite=SpriteLibControl.instance.GetSpriteByName(cf.Image);
    }
    public void OnItemClick()
    {
        Debug.LogError("Hiển thị Vũ khí");
        WeaponInfoDialogParam param = new WeaponInfoDialogParam { cf_gun = cf };
       // item_select.SetActive(true);
        DialogManager.instance.ShowDialog(DialogIndex.WeaponInfoDialog, param);
    }
  
   
}
