using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : BYSingletonMono<ConfigManager>
{
    
    public ConfigShop configShop;

    // Start is called before the first frame update
    public void InitConfig(Action callback)
    {
        StartCoroutine(ProgressLoadConfig(callback));
    }
    IEnumerator ProgressLoadConfig(Action callback)
    {
        configShop = Resources.Load("Config/ConfigShop", typeof(ScriptableObject)) as ConfigShop;
        yield return new WaitUntil(() => configShop != null);

  

        if (callback != null)
            callback();

        //callback?.Invoke();
    }
}
