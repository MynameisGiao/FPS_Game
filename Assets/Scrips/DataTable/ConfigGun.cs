using System;
using System.Collections.Generic;
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
    private string image;
    public string Image
    {
        get
        {
            return image;
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

    [SerializeField]
    private int fov_normal;
    public int  Fov_normal
    {
        get
        {
            return fov_normal;
        }

    }

    [SerializeField]
    private int fov_zoom;
    public int Fov_zoom
    {
        get
        {
            return fov_zoom;
        }

    }

    [SerializeField]
    private int price;
    public int Price
    {
        get
        {
            return price;
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

    public List<ConfigGunRecord> GetGunCollection() // lấy ra ds súng trừ những cây đã select (thuộc deck)
    {
        List<GunData> decks = DataController.instance.GetDeck();

        List<ConfigGunRecord> ls=new List<ConfigGunRecord>();
        foreach(ConfigGunRecord x in records)
        {
            bool isInDeck = false;
            foreach(GunData d in decks)
            {
                if(d.id==x.ID)
                {
                    isInDeck = true;
                }
            }
            if(isInDeck == false)
            {
                ls.Add(x);
            }
        }
        return ls;
    }
}
