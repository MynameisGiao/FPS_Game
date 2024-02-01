using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponItemSelect : MonoBehaviour
{
    public Image weapon_Icon;
    public Text name_lb;
    public GameObject btn_equip;
    private ConfigGunRecord config_gun;


    public void Setup(GunData data)
    {
        Debug.LogError("Dang load vu khi");
        config_gun = ConfigManager.instance.configGun.GetRecordBykeySearch(data.id);
        name_lb.text = config_gun.Name;
        weapon_Icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(config_gun.Image);

    }
}
