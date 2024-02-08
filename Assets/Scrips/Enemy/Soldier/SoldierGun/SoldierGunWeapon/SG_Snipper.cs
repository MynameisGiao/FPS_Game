using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Snipper : SG_Weapon_Behaviour
{
    public override void SetupGun(SoldierGunData soldierGunData)
    {
        Debug.LogError("Snipper!");
        this.data = soldierGunData;
        number_bullet = clip_size;
        i_SGHandle = new SG_SnipperHandle();
        i_SGHandle.Setup(this);
    }

    public void Reload()
    {
        StopCoroutine("ReloadProgress");
        StartCoroutine("ReloadProgress");
    }

    IEnumerator ReloadProgress()
    {
        isFire = false;
        isReloading = true;
        yield return new WaitForSeconds(time_reload);
        isReloading = false;
        number_bullet = clip_size;
        isFire = true;
    }
}

public class SG_SnipperHandle : ISoldierGunHandle
{
    SG_Snipper wp;
    public void FireHandle()
    {
        Debug.LogError("Snipper fire!");
    }

    public void ReloadHandle()
    {
        wp.Reload();
    }

    public void Setup(SG_Weapon_Behaviour sg_weapon)
    {
        this.wp = (SG_Snipper)sg_weapon;
    }
}