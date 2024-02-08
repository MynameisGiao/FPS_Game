using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierGunData
{
    public int damage;
    public float rof; // tốc độ bắn
  
}
public abstract class SG_Weapon_Behaviour : MonoBehaviour
{
    public ISoldierGunHandle i_SGHandle;
    public SoldierGunData data;
    public float time_reload;
    public float clip_size;
    public float number_bullet;
    private float time_fire;
    public bool isFire;
    public bool isReloading;
    public Transform player_target;
    public SG_MuzzleFlash muzzleFlash;
    public abstract void SetupGun(SoldierGunData soldierGunData);
    
    void Start()
    {
        number_bullet = clip_size;
    }

    public void Ready()
    {
        
    }
    void Update()
    {
        time_fire += Time.deltaTime;
        if(isFire&& !isReloading)
        {
            if(time_fire >= data.rof)
            {
                time_fire = 0;
                number_bullet--;
                muzzleFlash.Fire();
                i_SGHandle.FireHandle();
            }
        }
        if (number_bullet <= 0 && !isReloading)
        {
            i_SGHandle.ReloadHandle();
        }
    }

   
}

public interface ISoldierGunHandle
{
    void Setup(SG_Weapon_Behaviour sg_weapon);
    void FireHandle();
    void ReloadHandle();
}