using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public List<WeaponBehavior> weapons=new List<WeaponBehavior>();
    public WeaponBehavior current_wp;
    private int index_gun = -1;
    // Start is called before the first frame update
    void Start()
    {
        weapons.AddRange(gameObject.GetComponentsInChildren<WeaponBehavior>());
        foreach(WeaponBehavior wp in weapons)
        {
            wp.gameObject.SetActive(false);
            wp.Setup();
        }
        InputManager.instance.OnChangeGun.AddListener(OnChangeGun);
        InputManager.instance.OnZoom.AddListener(OnZoom);
        InputManager.instance.OnFire.AddListener(OnFire);
        InputManager.instance.OnReload.AddListener(OnReload);
        OnChangeGun();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnFire(bool isFire)
    {
        current_wp?.OnFire(isFire);
    }
    private void OnZoom()
    {
        current_wp?.OnZoom();
    }
    private void OnReload()
    {
        current_wp?.OnReload();
    }
    private void OnChangeGun()
    {
        index_gun++;
        if(index_gun >= weapons.Count)
            index_gun = 0;

        if (current_wp != null)
        {
            current_wp.OnHideGun(() =>
            {
                current_wp = weapons[index_gun];
                current_wp.OnReadyGun();
            });

        }
        else
        {
            current_wp = weapons[index_gun];
            current_wp.OnReadyGun();
        }

    }
}
