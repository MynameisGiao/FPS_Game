using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCollectionControl : MonoBehaviour
{
    public Transform collection_content;
    public WeaponItem prefab_item;
    private List<WeaponItem> items = new List<WeaponItem>();

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
        Setup();
    }

    public void Setup()
    {
        List<ConfigGunRecord> configGuns = ConfigManager.instance.configGun.records;
        if (items.Count == 0)
        {
            for (int i = 0; i < configGuns.Count; i++)
            {
                WeaponItem item = Instantiate(prefab_item);
                items.Add(item);
                item.transform.SetParent(collection_content, false);
            }
        }
        for (int i = 0; i < configGuns.Count; i++)
        {
            items[i].Setup(configGuns[i]);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
