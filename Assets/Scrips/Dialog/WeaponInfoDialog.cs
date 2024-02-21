using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoDialog : BaseDialog
{
    private WeaponInfoDialogParam dl_param;
    public Image weapon_icon;
    public Text name_lb;
    public TMP_Text rof;
    public TMP_Text clip_size;
    public TMP_Text damage;
    public TMP_Text recoil;
    public Text price_lb;
    public Button btn_unlock;
    public Button btn_select;
    public Button btn_selected;
    int gold;
    private ConfigGunRecord cf_gun;

    private GunData data;
    //private int index;
    private void OnEnable()
    {
        DataTrigger.RegisterValueChange(DataSchema.DECK, DeckDataChange);
        DataTrigger.RegisterValueChange(DataSchema.DIC_GUN, DeckDataChange);
    }

    private void OnDisable()
    {
        DataTrigger.UnRegisterValueChange(DataSchema.DECK, DeckDataChange);
        DataTrigger.UnRegisterValueChange(DataSchema.DIC_GUN, DeckDataChange);

    }

    private void DeckDataChange(object data)
    {
        Setup(dl_param);
    }

    public override void Setup(DialogParam param)
    {
        gold=DataController.instance.GetGold();
        dl_param=(WeaponInfoDialogParam)param;
        cf_gun=dl_param.cf_gun; 
        name_lb.text = cf_gun.Name.ToString();
        rof.text = "ROF: " + cf_gun.ROF.ToString();
        clip_size.text = "Clip Size: " + cf_gun.ClipSize.ToString();
        if (name_lb.text == "ShotGun")
        {
            damage.text = "Damage: " + (cf_gun.Damage * 5).ToString()+" / 5 bullets";
        }
        else
        {
            damage.text = "Damage: " + cf_gun.Damage.ToString();
        }
        recoil.text = "Recoil: " + cf_gun.Recoil.ToString();
       
        weapon_icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(cf_gun.Image);
        Debug.LogError("Load Weapon Done!!!");

        data = DataController.instance.GetGunData(cf_gun.ID);
        
       if (data == null)
        {
            price_lb.text = cf_gun.Price.ToString();
            btn_unlock.gameObject.SetActive(true);
            btn_select.gameObject.SetActive(false);
            btn_selected.gameObject.SetActive(false);


        }
        else
        {
            btn_unlock.gameObject.SetActive(false);
            btn_select.gameObject.SetActive(true);
            btn_selected.gameObject.SetActive(false);
        }

        List<GunData> decks = DataController.instance.GetDeck();
        for (int i = 0; i < decks.Count; i++)
        {
            if (cf_gun.ID == decks[i].id)
            {
                btn_unlock.gameObject.SetActive(false);
                btn_select.gameObject.SetActive(false);
                btn_selected.gameObject.SetActive(true);
            }
        }

    }

    public void OnSelect()
    {
        if (btn_select.gameObject.activeSelf == true)
        {
            DeckEquipDialogParam param = new DeckEquipDialogParam();
            param.gunData = data;
            DialogManager.instance.ShowDialog(DialogIndex.DeckEquipDialog, param);
            
        }

    }
    public void OnUnlock()
    {
        DataController.instance.UnlockWeapon(cf_gun, () =>
        {

        });
    }

}
