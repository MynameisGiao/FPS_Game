using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IngameView : BaseView
{
    public Text wave_lb;
    public Image hp_fg; // 654
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
        hp_fg.fillAmount = 1;
        MissionManager.instance.OnWaveChange += OnWaveChange;
        MissionManager.instance.OnHpChange += OnHpChange;
    }

    private void OnHpChange(int damage, int max_hp, int cur_hp)
    {
        cur_hp -= damage; // Giảm sức khỏe hiện tại theo lượng damage nhận vào

        if (cur_hp < 0)
        {
            cur_hp = 0; // Đảm bảo sức khỏe không âm
        }

        float val = (float)cur_hp / (float)max_hp;
        hp_fg.fillAmount = val;
    }

    private void OnWaveChange(int arg1, int arg2)
    {
        wave_lb.text = "WAVE: " + arg1.ToString() + "/" + arg2.ToString();
    }

    public override void OnHideView()
    {
        MissionManager.instance.OnHpChange += OnHpChange;
        MissionManager.instance.OnWaveChange -= OnWaveChange;
    }
    
}
