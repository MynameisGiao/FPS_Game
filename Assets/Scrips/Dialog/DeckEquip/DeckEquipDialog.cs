using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckEquipDialog : BaseDialog
{
    public Image weapon_Icon;
    public Text name_lb;

    public List<WeaponItemEquip> deck_items; // item trong deck là weapon
    public GunData cur_GunData;
    
    public override void Setup(DialogParam param)
    {
        DeckEquipDialogParam dl_param = (DeckEquipDialogParam)param;
        cur_GunData = dl_param.gunData;

        List<GunData> decks = DataController.instance.GetDeck();
        Debug.LogError("Vào DECK!!!!! " + decks.Count);
        for (int i = 0; i < decks.Count; i++)
        {
            deck_items[i].Setup(decks[i], cur_GunData,i);
        }

        ConfigGunRecord config_gun = ConfigManager.instance.configGun.GetRecordBykeySearch(cur_GunData.id);
        name_lb.text = config_gun.Name;
        weapon_Icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(config_gun.Image);

    }


    public void OnExit()
    {
        DialogManager.instance.HideDialog(DialogIndex.DeckEquipDialog);
    }

}
