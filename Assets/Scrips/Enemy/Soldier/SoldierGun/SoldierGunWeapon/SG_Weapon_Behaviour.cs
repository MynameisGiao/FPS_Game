using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierGunData
{
    public int damage;
    public float rof; // tốc độ bắn
    public SoldierGunControl sg_Control;
  
}
public abstract class SG_Weapon_Behaviour : MonoBehaviour
{
    public ISoldierGunHandle i_SGHandle;
    public SoldierGunData data;
   
    private float time_fire;
    public bool isFire;
 
    public Transform player_target;
    public SG_MuzzleFlash muzzleFlash;
    public abstract void SetupGun(SoldierGunData soldierGunData);
    
    void Start()
    {
       
    }

    public void Ready()
    {
        
    }
    void Update()
    {
        time_fire += Time.deltaTime;
        if(isFire)
        {
            if(time_fire >= data.rof)
            {
                time_fire = 0;
              
                muzzleFlash.Fire();
                i_SGHandle.FireHandle();
                data.sg_Control.dataBinding.Attack = true;
            }
        }
       
    }

   
}

public interface ISoldierGunHandle
{
    void Setup(SG_Weapon_Behaviour sg_weapon);
    void FireHandle();
    void ReloadHandle();
}