using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiddleRightController : MonoBehaviour
{
    public RectTransform parent;
    public Text voucher_lb;
    public Text gold_lb;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == 1)
        {
            PlayerInfo playerInfo = DataController.instance.GetPlayerInfo();
            voucher_lb.text = DataController.instance.GetVoucher().ToString();
            gold_lb.text = DataController.instance.GetGold().ToString();
            parent.DOAnchorPosY(0, 1);

        }
        else
        {
            parent.DOAnchorPosY(510, 1);

        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
