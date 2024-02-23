using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class TopBarController : MonoBehaviour
{
    public RectTransform parent;
    public Text nick_lb;
    public Text voucher_lb;
    public Text gold_lb;
    private int gold;
    private Tweener tween_gold;

    public GameObject Left_obj; 
    public GameObject Setting_obj;

    private DataModel dataModel;

    private void Awake()
    {
        dataModel = DataModel.Instance;

        dataModel.OnNicknameUpdated += UpdateNicknameUI;
    }
    private void UpdateNicknameUI(string newNickname)
    {
        nick_lb.text = newNickname;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        ViewManager.instance.OnViewShow += ViewManager_OnViewShow;
        ViewManager.instance.OnViewHide += Instance_OnViewHide;
    }

    private void Instance_OnViewHide(BaseView obj)
    {
        if (obj.viewIndex == ViewIndex.HomeView )
        {

            parent.DOAnchorPosY(500,1);
           
        }
    }

    private void ViewManager_OnViewShow(BaseView obj)
    {
        
            if (obj.viewIndex == ViewIndex.HomeView || obj.viewIndex == ViewIndex.MissionView)
            {
                parent.DOAnchorPosY(0, 0.5f);
                Left_obj.SetActive(true);
                Setting_obj.SetActive(true);
            }
            else if (obj.viewIndex == ViewIndex.ShopView)
            {
                parent.DOAnchorPosY(0, 0.5f);
                Left_obj.SetActive(false);
                Setting_obj.SetActive(false);
            }
            else if (obj.viewIndex == ViewIndex.IngameView)
            {
                // Ẩn tất cả UI trong parent
                parent.DOAnchorPosY(500, 1);
                Left_obj.SetActive(false);
                Setting_obj.SetActive(false);
            }
        
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex==1)
        {
            PlayerInfo playerInfo = DataController.instance.GetPlayerInfo();
            nick_lb.text = playerInfo.nickname;
            voucher_lb.text = DataController.instance.GetVoucher().ToString();
            gold=DataController.instance.GetGold();
            gold_lb.text = gold.ToString();
            DataTrigger.RegisterValueChange(DataSchema.INVENTORY, DataGoldChange);
           
        }
       
    }
    private void DataInfoChange(object data)
    {
        string newNickname = data.ToString();
        nick_lb.text = newNickname; // Cập nhật UI với nick name mới
        DataModel.Instance.UpdateNickname(newNickname); // Cập nhật nick name trong DataModel
    }
    private void DataGoldChange(object data)
    {
        int cur_gold = gold;

        gold = DataController.instance.GetGold();
        if(tween_gold != null)
        {
            tween_gold.Kill();
        }
        tween_gold = DOTween.To(() => cur_gold, x => cur_gold = x, gold, 0.5f).OnUpdate(() =>
        {
            gold_lb.text = cur_gold.ToString();
        });

        
    }
    public void AddGold()
    {
        DialogManager.instance.HideAllDialog();
        ViewManager.instance.SwitchView(ViewIndex.MissionView);
    }

    void Update()
    {

    }
    private void OnDisable()
    {
        DataTrigger.UnRegisterValueChange(DataSchema.GOLD, DataGoldChange);
    }

   
}
