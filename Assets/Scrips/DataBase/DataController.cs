using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class DataController : BYSingletonMono<DataController>
{
    public DataModel dataModel;
    public void InitData(Action callback)
    {

        dataModel.InitData(callback);
        DataTrigger.RegisterValueChange(DataSchema.DIC_GUN, (dic) =>
        {
            Debug.LogError(" DIC_GUN  change");
        });
        DataTrigger.RegisterValueChange(DataSchema.DIC_GUN + "/K_3", (unit) =>
        {
            Debug.LogError(" gun change");
        });
    }
    public void CreateMissionData()
    {
         
        Dictionary<string, GunData> dicGun = dataModel.ReadData<Dictionary<string, GunData>>(DataSchema.DIC_GUN);
        int id = 2;
        GunData unit = dataModel.ReadDicData<GunData>(DataSchema.DIC_GUN, id.Tokey());
        string s = JsonConvert.SerializeObject(unit);
        Debug.LogError(s);

    }
    public PlayerInfo GetPlayerInfo()
    {
        PlayerInfo info = dataModel.ReadData<PlayerInfo>(DataSchema.INFO);
        return info;
    }
    public int GetVoucher()
    {
        return dataModel.ReadData<int>(DataSchema.VOUCHER);
    }
    public int GetGold()
    {
        return dataModel.ReadData<int>(DataSchema.GOLD);
    }
    public void AddGold(int number)
    {
        int gold = GetGold();
        gold += number;
        if (gold < 0)
            gold = 0;
        dataModel.UpdateData(DataSchema.GOLD, gold);
    }
    public void AddVoucher(int number)
    {
        int voucher = GetVoucher();
        voucher += number;
        if (voucher < 0)
            voucher = 0;
        dataModel.UpdateData(DataSchema.VOUCHER, voucher);
    }
    
    public GunData GetGunData(int id)
    {
        return dataModel.ReadDicData<GunData>(DataSchema.DIC_GUN, id.Tokey());
    }
    public void UnlockWeapon(ConfigGunRecord configGunRecord, Action callback)
    {
        GunData gunData = GetGunData(configGunRecord.ID);
        if (gunData == null)
        {
            gunData = new GunData();
            gunData.id = configGunRecord.ID;
           
            int gold = GetGold();
            int price = configGunRecord.Price;
            if (gold >= price)
            {
                gold -= price;
                dataModel.UpdateData(DataSchema.GOLD, gold);
                dataModel.UpdateDicData<GunData>(DataSchema.DIC_GUN, gunData.id.Tokey(), gunData);

            }
        }
        callback();
    }

    public void OnGetReward(ConfigMissionRecord cf)
    {
        AddGold(cf.Reward);
        Debug.LogError("Reward"+ cf.Reward);
    }
    public List<GunData> GetDeck()
    {
        return dataModel.ReadData<List<GunData>>(DataSchema.DECK);
    }
    public void ChangeDeck(GunData gunData, int index)
    {
        List<GunData> deck = dataModel.ReadData<List<GunData>>(DataSchema.DECK);
        deck[index] = gunData;
        dataModel.UpdateData(DataSchema.DECK, deck);
    }
}
