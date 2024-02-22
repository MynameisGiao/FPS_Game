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
    public  GameObject take_damage;
    public  GameObject plus_hp;
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        take_damage.SetActive(false);
        plus_hp.SetActive(false);
        hp_fg.color = Color.white;
       ViewManager.instance.music.enabled = false;
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
        MissionManager.instance.SetIngameViewReference(this);
        MissionManager.instance.OnWaveChange.AddListener(OnWaveChange);
        MissionManager.instance.OnHpChange.AddListener(OnHpChange);

    }

    private void OnHpChange(int damage, int max_hp, int cur_hp)
    { 
        cur_hp -= damage;
       
       
        if (cur_hp < 0)
        {
            cur_hp = 0;
        }

        float val = (float)cur_hp / (float)max_hp;
        hp_fg.fillAmount = val;

        if (cur_hp <= 70)
        {
            if (cur_hp <= 30)
            {

                hp_fg.color = Color.red;
            }
            else
            {

                hp_fg.color = new Color(1f, 0.502f, 0.447f); // Mã màu #FA8072
            }
        }
        else
        {
            hp_fg.color = Color.white;
        }
    }

    private void OnWaveChange(int arg1, int arg2)
    {
        wave_lb.text = "Wave: " + arg1.ToString() + "/" + arg2.ToString();
    }

    public override void OnHideView()
    {
        ViewManager.instance.music.enabled = true;
    }

   
}
