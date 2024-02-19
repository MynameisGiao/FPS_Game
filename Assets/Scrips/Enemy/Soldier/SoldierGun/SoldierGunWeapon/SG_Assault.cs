using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Assault : SG_Weapon_Behaviour
{
    public override void SetupGun(SoldierGunData soldierGunData)
    {

        this.data = soldierGunData;
      
        i_SGHandle = new SG_AssaultHandle();
        i_SGHandle.Setup(this);
    }
    
}

public class SG_AssaultHandle : ISoldierGunHandle
{
    SG_Assault wp;
    public void FireHandle()
    {
    }
    public void ReloadHandle()
    {
       
    }
    public void Setup(SG_Weapon_Behaviour sg_weapon)
    {
        this.wp = (SG_Assault)sg_weapon;
    }
}