using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNameDialog : BaseDialog
{
    public InputField nicknameInput;
    private DataModel dataModel;

    public AudioSource sfx;
    public override void Setup(DialogParam param)
    {
        base.Setup(param);
       sfx.enabled = false;
    }
    void Start()
    {
        dataModel = DataModel.Instance;
       
    }

    public void OnSelect()
    {
        sfx.enabled = true;
        if (dataModel != null) 
        {
            string newNickname = nicknameInput.text;
            dataModel.UpdateNickname(newNickname);
            DialogManager.instance.HideDialog(DialogIndex.InputNameDialog);
        }
        else
        {
            Debug.LogError("DataModel is null!");
        }
       
    }

    public void OnClose()
    {
        DialogManager.instance.HideDialog(DialogIndex.InputNameDialog);
    }
}
