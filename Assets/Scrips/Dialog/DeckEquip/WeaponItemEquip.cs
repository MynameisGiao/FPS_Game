using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItemEquip : MonoBehaviour
{
    public Image weapon_Icon;
    public Text name_lb; 
    private GunData cur_gunData;
    private ConfigGunRecord config_gun;
    private int index;

    public AudioSource sfx;

    public void Setup(GunData data, GunData cur_gunData, int index)
    {
        this.cur_gunData = cur_gunData;
        this.index = index;
      

        config_gun = ConfigManager.instance.configGun.GetRecordBykeySearch(data.id);
        name_lb.text = config_gun.Name;
        weapon_Icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(config_gun.Image);

        sfx.enabled = false;
    }
    public void OnSelect()
    {
        sfx.enabled=true;
        DialogManager.instance.HideDialog(DialogIndex.DeckEquipDialog);
        DataController.instance.ChangeDeck(cur_gunData, index);


    }
}
