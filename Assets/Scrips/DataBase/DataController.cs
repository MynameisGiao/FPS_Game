using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataController : BYSingletonMono<DataController>
{
    // data model 
    public DataModel dataModel;
    // Start is called before the first frame update
    public void InitData(Action callback)
    {
        dataModel.InitData(callback);
        DataTrigger.RegisterValueChange(DataSchema.DIC_UNIT, (dic) =>
        {
            Debug.LogError(" DIC_UNIT  change");
        });
        DataTrigger.RegisterValueChange(DataSchema.DIC_UNIT+"/K_3", (unit) =>
        {
            Debug.LogError(" unit change"); 
        });
    }
    public void CreateMissionData()
    {

        Dictionary<string, UnitData> dicUnit = dataModel.ReadData<Dictionary<string, UnitData>>(DataSchema.DIC_UNIT);

        int id = 2;
        UnitData unit = dataModel.ReadDicData<UnitData>(DataSchema.DIC_UNIT, id.Tokey());
        string s = JsonConvert.SerializeObject(unit);
        Debug.LogError(s);
    
    }
    public PlayerInfo GetPlayerInfo()
    {
        PlayerInfo info = dataModel.ReadData<PlayerInfo>(DataSchema.INFO);
        return info;
    }
    public int GetGem()
    { 
        return dataModel.ReadData<int>(DataSchema.GEM);
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
    public void AddGem(int number)
    {
        int gem = GetGem();
        gem += number;
        if (gem < 0)
            gem = 0;
        dataModel.UpdateData(DataSchema.GEM, gem);
    }
    public void UpdateUnitLevel(int id)
    {
        UnitData unit = dataModel.ReadDicData<UnitData>(DataSchema.DIC_UNIT, id.Tokey());
        unit.level++;
        dataModel.UpdateDicData<UnitData>(DataSchema.DIC_UNIT, id.Tokey(), unit);

    }
    public UnitData GetUnitData(int id)
    {
        return dataModel.ReadDicData<UnitData>(DataSchema.DIC_UNIT, id.Tokey()) ;
    }
    
    public void OnShopBuy(ConfigShopRecord cf)
    {
        if(cf.Shop_type==1)
        {
            AddGold(cf.Value);
        }
        else
        {
            AddGem(cf.Value);
        }
    }
    public List<UnitData> GetDeck()
    {
        return dataModel.ReadData<List<UnitData>>(DataSchema.DECK);
    }
    public void ChangeDeck(UnitData unitData,int index)
    {
        List<UnitData> deck= dataModel.ReadData<List<UnitData>>(DataSchema.DECK);
        deck[index] = unitData;
        dataModel.UpdateData(DataSchema.DECK, deck);
    }
}
