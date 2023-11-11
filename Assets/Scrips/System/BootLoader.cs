using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : MonoBehaviour
{
   
    // Start is called before the first frame update
    IEnumerator Start()
    {
        DontDestroyOnLoad(gameObject);
        yield return new WaitForSeconds(1);
        ConfigManager.instance.InitConfig(InitData);
       
     
    }
    private void InitData()
    {
        DataController.instance.InitData(() =>
        {
            LoadSceneManager.instance.LoadSceneByName("Buffer", LoadSceneDone);
        });
    }
   

    public void LoadSceneDone()
    {
        Debug.LogError(" load scene done");
        //ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
}
