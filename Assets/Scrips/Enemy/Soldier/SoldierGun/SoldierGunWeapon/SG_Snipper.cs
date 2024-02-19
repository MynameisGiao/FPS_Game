using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_Snipper : SG_Weapon_Behaviour
{
    public override void SetupGun(SoldierGunData soldierGunData)
    {
        this.data = soldierGunData;
       
        i_SGHandle = new SG_SnipperHandle();
        i_SGHandle.Setup(this);
    }

    
}

public class SG_SnipperHandle : ISoldierGunHandle
{
    SG_Snipper wp;
    public void FireHandle()
    {
     
    }

    public void ReloadHandle()
    {
        
    }

    public void Setup(SG_Weapon_Behaviour sg_weapon)
    {
        this.wp = (SG_Snipper)sg_weapon;
    }
}