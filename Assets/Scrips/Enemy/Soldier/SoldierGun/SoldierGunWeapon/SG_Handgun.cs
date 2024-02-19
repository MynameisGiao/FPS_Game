using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Handgun : SG_Weapon_Behaviour
{
    public override void SetupGun(SoldierGunData soldierGunData)
    {
        this.data= soldierGunData;
       
        i_SGHandle = new SG_HandGunHandle();
        i_SGHandle.Setup(this);
    }

   
}

public class SG_HandGunHandle : ISoldierGunHandle
{
    SG_Handgun wp;
    public void FireHandle()
    {
    }

    public void ReloadHandle()
    {
        
    }

    public void Setup(SG_Weapon_Behaviour sg_weapon)
    {
        this.wp = (SG_Handgun)sg_weapon;
    }
}