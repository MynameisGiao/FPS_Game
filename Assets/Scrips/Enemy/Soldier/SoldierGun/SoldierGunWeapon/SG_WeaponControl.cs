using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SG_WeaponControl : MonoBehaviour
{
   

    public List<SG_Weapon_Behaviour> weapons;
    void Start()
    {
        foreach(SG_Weapon_Behaviour gun in weapons)
        {
            gun.SetupGun(new SoldierGunData());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeGun()
    {

    }
}
