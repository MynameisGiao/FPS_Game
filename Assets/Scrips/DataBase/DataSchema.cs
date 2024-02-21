using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSchema
{
    public const string INFO = "info";
    public const string INVENTORY = "inventory";
    public const string GOLD = "inventory/gold";
    public const string VOUCHER = "inventory/voucher";
    public const string DIC_GUN = "inventory/dic_gun";
    public const string DECK = "info/deck";
}
[Serializable] // parse Json
public class PlayerData
{
    [SerializeField]
    public PlayerInfo info;
    [SerializeField]
    public PlayerInventory inventory;
    public PlayerMissionData missionData;
}
[Serializable]
public class PlayerInfo
{
    public string nickname;
    [SerializeField]
    public List<GunData> deck = new List<GunData>();
}
[Serializable]
public class PlayerInventory
{
    public int gold;
    public int voucher;
    [SerializeField]
    public Dictionary<string, GunData> dic_gun = new Dictionary<string, GunData>();
}
[Serializable]
public class PlayerMissionData
{
    public int currentMission;
    [SerializeField]
    public Dictionary<string, MissionData> dic_mission = new Dictionary<string, MissionData>();
}
[Serializable]
public class MissionData
{
    public int id;
}

[Serializable]
public class GunData
{
    public int id;
}
