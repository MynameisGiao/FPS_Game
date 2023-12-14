using System;
using UnityEngine;
[Serializable]
public class ConfigGunRecord
{
    [SerializeField]
    private int id;
    public int ID
    {
        get
        {
            return id;
        }
    }

    [SerializeField]
    private string name;
    public string Name
    {
        get
        {
            return name;
        }
    }

    [SerializeField]
    private string prefab;
    public string Prefab
    {
        get
        {
            return prefab;
        }
    }
    [SerializeField]
    private float rof;
    public float ROF
    {
        get
        {
            return rof;
        }
    }

    [SerializeField]
    private int clipsize;
    public int ClipSize
    {
        get
        {
            return clipsize;
        }

    }
    [SerializeField]
    private int total;
    public int Total
    {
        get
        {
            return total;
        }

    }
    [SerializeField]
    private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }

    }
    [SerializeField]
    private float recoil;
    public float Recoil
    {
        get
        {
            return recoil;
        }

    }
}
public class ConfigGun : BYDataTable<ConfigGunRecord>
{
    public override ConfigCompare<ConfigGunRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigGunRecord>("id");
        return configCompare;
    }


}
