using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopView : BaseView
{
    // đầu game load nhưngx item đã select
    public DeckItemControl deckItemControl_;  
    public DeckCollectionControl deckCollectionControl_;
    public Transform ShowWeapon;
   
    public override void Setup(ViewParam param)
    {
        
        base.Setup(param);
        deckItemControl_.Setup();
        deckCollectionControl_.Setup();

        deckItemControl_.gameObject.SetActive(false);
        
      
    }
    public void ShowHomeView()
    {
        DialogManager.instance.HideDialog(DialogIndex.WeaponInfoDialog);
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
        
    }
    
}
