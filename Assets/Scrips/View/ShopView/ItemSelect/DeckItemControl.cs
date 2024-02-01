using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckItemControl : MonoBehaviour
{
    public List<WeaponItemSelect> deck_items; // item trong deck là weapon

    private void OnEnable()
    {
        DataTrigger.RegisterValueChange(DataSchema.DECK, DeckDataChange);
    }

    private void OnDisable()
    {
        DataTrigger.UnRegisterValueChange(DataSchema.DECK, DeckDataChange);

    }

    private void DeckDataChange(object data)
    {
        Setup();
    }
    public void Setup()
    {
        
        List<GunData> decks = DataController.instance.GetDeck();
        for (int i=0; i<decks.Count; i++)
        {
            deck_items[i].Setup(decks[i]);
        }
    }
}
