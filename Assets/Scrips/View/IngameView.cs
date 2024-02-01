using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class IngameView : BaseView
{
    public TextAlignment wave_lb;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        Debug.LogError("Ingame View!!!");
       
    }

 
        void Update()
    {
        // Check for the "Escape" key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
    }

    public void OnPause()
    {
        DialogManager.instance.ShowDialog(DialogIndex.PauseDialog);

    }

    public override void OnShowView()
    {
        MissionManager.instance.OnWaveChange += OnWaveChange;
    }

    private void OnWaveChange(int arg1, int arg2)
    {
       Debug.LogError("WAVE: "+arg1.ToString()+ "/"+ arg2.ToString());
    }

    public override void OnHideView()
    {
        
        MissionManager.instance.OnWaveChange -= OnWaveChange;
    }
    
}
