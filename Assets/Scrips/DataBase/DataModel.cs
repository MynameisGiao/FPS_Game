using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

public class DataEventTrigger : UnityEvent<object>
{

}
public static class DataTrigger
{
    public static Dictionary<string, DataEventTrigger> dicOnvaluaChange = new Dictionary<string, DataEventTrigger>();
    public static void RegisterValueChange(string path, UnityAction<object> delegateValueChange)
    {
        if (dicOnvaluaChange.ContainsKey(path))
        {
            dicOnvaluaChange[path].AddListener(delegateValueChange);
        }
        else
        {
            dicOnvaluaChange.Add(path, new DataEventTrigger());
            dicOnvaluaChange[path].AddListener(delegateValueChange);
        }
    }
    public static void UnRegisterValueChange(string path, UnityAction<object> delegateValueChange)
    {
        if (dicOnvaluaChange.ContainsKey(path))
        {
            dicOnvaluaChange[path].RemoveListener(delegateValueChange);
        }
    }
    public static void TrggerEventData(this object data, string path)
    {
        if (dicOnvaluaChange.ContainsKey(path))
        {
            dicOnvaluaChange[path].Invoke(data);
        }
    }
}

public class DataModel : MonoBehaviour
{
    private static DataModel _instance;
    public static DataModel Instance { get { return _instance; } }

    private PlayerData playerData;
    public List<GunData> deck;

    public event Action<string> OnNicknameUpdated;

   
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void InitData(Action callback)
    {
        if (!LoadData())
        {

            playerData = new PlayerData();
            PlayerInfo info = new PlayerInfo();
            info.nickname = PlayerPrefs.GetString("PlayerNickname", "PlayerName");
            info.deck = deck;
            playerData.info = info;

            PlayerInventory inventory = new PlayerInventory();
            inventory.gold = 100;
            inventory.voucher = 10;
            Dictionary<string, GunData> dic = new Dictionary<string, GunData>();
            foreach (GunData gun in deck)
            {
                dic.Add(gun.id.Tokey(), gun); ;
            }
            inventory.dic_gun = dic;
            playerData.inventory = inventory;

            PlayerMissionData missionData = new PlayerMissionData();
            missionData.currentMission = 1;
            playerData.missionData = missionData;
            SaveData();
            callback();
        }
        else
        {
            callback();
        }

    }

    public T ReadData<T>(string path)
    {
        object data = null;
        // path: inventory/gold=> list 2 phan tu 

        List<string> paths = path.MakeListPath();

        ReadDataByPath(paths, playerData, out data);

        return (T)data;
    }

    public T ReadDicData<T>(string path, string key)
    {
        List<string> paths = path.MakeListPath();
        object data = null;
        ReadDataByPath(paths, playerData, out data);
        Dictionary<string, T> dic = (Dictionary<string, T>)data;
        T dataElement;
        dic.TryGetValue(key, out dataElement);

        return dataElement;
    }
    private void ReadDataByPath(List<string> paths, object data, out object outData)
    {
        outData = null;
        // 1-> inventory
        //2 -> gold
        string p = paths[0];
        // data<=> playerData
        Type t = data.GetType();
        FieldInfo field = t.GetField(p);
        if (paths.Count == 1)
        {
            outData = field.GetValue(data);
        }
        else
        {
            paths.RemoveAt(0);
            ReadDataByPath(paths, field.GetValue(data), out outData);
        }
    }
    public void UpdateData(string path, object dataNew, Action callback = null)
    {
        List<string> paths = path.MakeListPath();
        List<object> dataChange = new List<object>();
        UpdateDataByPath(paths, playerData, dataNew, ref dataChange, callback);
        paths.Clear();
        string s_path = string.Empty;
        paths = path.MakeListPath();
        for (int i = 0; i < paths.Count; i++)
        {
            if (i == 0)
            {
                s_path = paths[i];
            }
            else
            {
                s_path = s_path + "/" + paths[i];
            }
            dataChange[i].TrggerEventData(s_path);
        }
        SaveData();
    }
    private void UpdateDataByPath(List<string> paths, object data, object dataNew, ref List<object> dataChange, Action callback = null)
    {
        // 1-> inventory
        //2 -> gold
        string p = paths[0];
        // data<=> playerData
        Type t = data.GetType();
        FieldInfo field = t.GetField(p);
        if (paths.Count == 1)
        {
            dataChange.Add(dataNew);
            field.SetValue(data, dataNew);
            callback?.Invoke();
        }
        else
        {
            dataChange.Add(field.GetValue(data));
            paths.RemoveAt(0);
            UpdateDataByPath(paths, field.GetValue(data), dataNew, ref dataChange, callback);
        }
    }
    public void UpdateDicData<T>(string path, string key, T dataNew, Action callback = null)
    {
        List<string> paths = path.MakeListPath();
        object dataReturn = UpdateDicDataByPath<T>(paths, key, playerData, dataNew, callback);
        paths.Clear();
        SaveData();
        dataReturn.TrggerEventData(path);
        dataNew.TrggerEventData(path + "/" + key);
    }
    private object UpdateDicDataByPath<T>(List<string> paths, string key, object data, T dataNew, Action callback = null)
    {
        object dataReturn = null;
        // 1-> inventory
        //2 -> gold
        string p = paths[0];
        // data<=> playerData
        Type t = data.GetType();
        FieldInfo field = t.GetField(p);
        if (paths.Count == 1)
        {
            Dictionary<string, T> dic = (Dictionary<string, T>)field.GetValue(data);
            dic[key] = dataNew;
            dataReturn = dic;
            field.SetValue(data, dic);
            callback?.Invoke();
        }
        else
        {
            paths.RemoveAt(0);
            UpdateDicDataByPath<T>(paths, key, field.GetValue(data), dataNew, callback);
        }
        return dataReturn;
    }

    // Hàm cập nhật nick name
    public void UpdateNickname(string newNickname)
    {
        if (string.IsNullOrEmpty(newNickname))
        {
            newNickname = "PlayerName";
        }

        Debug.LogError("Nickname: " + newNickname);
        playerData.info.nickname = newNickname;
        SaveData();

        OnNicknameUpdated?.Invoke(newNickname);
    }



    private bool LoadData()
    {
        if (PlayerPrefs.HasKey("LOCAL_DATA"))
        {
            string s_data = PlayerPrefs.GetString("LOCAL_DATA");
            playerData = JsonConvert.DeserializeObject<PlayerData>(s_data);
            return true;
        }
        return false;
    }
    private void SaveData()
    {
        string s_data = JsonConvert.SerializeObject(playerData);

        PlayerPrefs.SetString("LOCAL_DATA", s_data);
    }
}
