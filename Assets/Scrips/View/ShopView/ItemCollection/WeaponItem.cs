using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    public Image weapon_Icon;
    public Text name_lb;  
    public ShopView shopView;
    public GameObject item_lock;
    public GameObject item_select;
    private ConfigGunRecord config_gun;
    private GunData data;
  
    public void Setup(ConfigGunRecord cf)
    {
       
        this.config_gun = cf;
        name_lb.text = cf.Name.ToString();
        weapon_Icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(cf.Image);

        data = DataController.instance.GetGunData(cf.ID);

        item_lock.SetActive(data == null); // check dữ liệu thuộc sở hữu để lock
        item_select.SetActive(false);

        // Kiểm tra ID để bật item_select -> auto select handgun
        List<GunData> decks = DataController.instance.GetDeck();
        for (int i = 0; i < decks.Count; i++)
        {
           if(cf.ID== decks[i].id)
            {
                item_select.SetActive(true);
            }
        }
    }
    public void OnItemClick()
    {
        Debug.LogError("Hiển thị Vũ khí");
        WeaponInfoDialogParam param = new WeaponInfoDialogParam { cf_gun = config_gun };
        DialogManager.instance.ShowDialog(DialogIndex.WeaponInfoDialog, param);
    }

   

}
