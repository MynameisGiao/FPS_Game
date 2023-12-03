using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigMissionRecord
{
    // id	stage	name	description	sceneName	enemies	reward
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
    private int stage;
    public int Stage
    {
        get
        {
            return stage;
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
    private string description;
    public string Description
    {
        get
        {
            return description;
        }
    }
    [SerializeField]
    private string sceneName;
    public string SceneName
    {
        get
        {
            return sceneName;
        }
    }
    [SerializeField]
    private string enemies;
    public List<int> Enemies

    {
        get
        {
            string[] s = enemies.Split(';');
            List<int> ls = new List<int>();
            foreach (string e in s)
            {
                ls.Add(int.Parse(e));
            }
            return ls;
        }
    }
    [SerializeField]
    private int reward;
    public int Reward
    {
        get
        {
            return reward;
        }
    }
}
public class ConfigMission : BYDataTable<ConfigMissionRecord>
{
    public override ConfigCompare<ConfigMissionRecord> DefindCompare()
    {
        configCompare = new ConfigCompare<ConfigMissionRecord>("id");
        return configCompare;
    }
}
