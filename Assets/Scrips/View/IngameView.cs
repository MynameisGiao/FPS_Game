using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class IngameView : BaseView
{
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
    

}
