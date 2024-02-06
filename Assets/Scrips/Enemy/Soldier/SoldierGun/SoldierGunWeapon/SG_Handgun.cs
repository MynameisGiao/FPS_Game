using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Handgun : SG_Weapon_Behaviour
{
    public override void SetupGun(SoldierGunData soldierGunData)
    {
        Debug.LogError("handgun!");
        this.data= soldierGunData;
       
        i_SGHandle = new SG_HandGunHandle();
        i_SGHandle.Setup(this);
    }

    public void Reload()
    {
        StopCoroutine("ReloadProgress");
        StartCoroutine("ReloadProgress");
    }

    IEnumerator ReloadProgress()
    {
        isReloading = true;
        yield return new WaitForSeconds(time_reload);
        isReloading= false;
        number_bullet = clip_size;
    }
}

public class SG_HandGunHandle : ISoldierGunHandle
{
    SG_Handgun wp;
    public void FireHandle()
    {
        Debug.LogError("handgun fire!");
    }

    public void ReloadHandle()
    {
        wp.Reload();
    }

    public void Setup(SG_Weapon_Behaviour sg_weapon)
    {
        this.wp = (SG_Handgun)sg_weapon;
    }
}