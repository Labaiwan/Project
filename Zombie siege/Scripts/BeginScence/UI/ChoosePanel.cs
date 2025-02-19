using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChoosePanel : BasePanel
{
    public Text txtMoney;//拥有的金钱数量
    public Text txtTips;//人物描述

    public Button btnLeft;
    public Button btnRight;

    public Button btnStart;
    public Button btnBack;

    public Button btnUnLock;
    public Text txtLockMoney;

    private Transform heroPos;

    private GameObject player = null;
    private int index = 0;//当前显示角色的索引
    private int playerNum = 3;//角色的数量
    private RoleInfo currentRole = null;//当前角色

    protected override void Init()
    {
        //显示金钱数量
        txtMoney.text = DataMgr.Instance.playerData.money.ToString();
        //获取到角色生成的位置
        heroPos = GameObject.Find("heroPos").transform;

        UpdateRole();

        //按钮事件监听
        btnLeft.onClick.AddListener(()=>
        {
            //向前翻页 选择人物
            index = (index - 1+ playerNum)% playerNum;
            //根据索引更新角色
            UpdateRole();
        });


        btnRight.onClick.AddListener(()=>
        {
            //向后翻页 选择人物
            index = (index + 1) % playerNum;
            //根据索引更新角色
            UpdateRole();

        });

        btnStart.onClick.AddListener(()=>
        {
            //记录选择好的角色
            DataMgr.Instance.CurrentPlayer = DataMgr.Instance.roleInfos[index];
            //隐藏当前面板 显示选择场景面板
            UIManager.Instance.HidePanel<ChoosePanel>();
            UIManager.Instance.ShowPanel<ChooseScenePanel>();
        });

        btnBack.onClick.AddListener(()=>
        {
            //隐藏面板（删除已经创建好的角色）
            UIManager.Instance.HidePanel<ChoosePanel>();

            //摄像头右转 显示开始面板
            //播放摄像机动画 左转
            Camera.main.GetComponent<CameraAnim>().TurnRight(() =>
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
                //删除掉之前的角色
                if (player != null)
                {
                    DestroyImmediate(player);
                    player = null;
                }
            });
            
        });

        btnUnLock.onClick.AddListener(()=>{

            currentRole = DataMgr.Instance.roleInfos[index];

            //判断是否满足购买条件
            if (DataMgr.Instance.playerData.money > currentRole.lockMoney)
            {
                DataMgr.Instance.playerData.money -= currentRole.lockMoney;
                txtMoney.text = DataMgr.Instance.playerData.money.ToString();
                DataMgr.Instance.playerData.buyHero.Add(index);

                //保存玩家数据
                DataMgr.Instance.SavePlayerData();
                UpdateUnLock();
                //提示购买成功
                UIManager.Instance.ShowPanel<TipPanel>().SetContent("购买成功！");

            }
            else
            {
                //提示购买失败
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.SetContent("金钱数量不足");
            }
        });
    }

    public void UpdateRole()
    {
        //更显前删除掉之前的角色
        if (player != null)
        {
            Destroy(player);
            player = null;
        } 
        //显示角色
        player = GameObject.Instantiate(Resources.Load<GameObject>(DataMgr.Instance.roleInfos[index].res), heroPos.position,heroPos.rotation);
        Destroy(player.GetComponent("Player"));
        //调整位置
        /*player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
        player.transform.localScale = Vector3.one;*/
        //显示角色的描述
        txtTips.text = DataMgr.Instance.roleInfos[index].tips;
        //显示角色的解锁所需的钱
        txtLockMoney.text = DataMgr.Instance.roleInfos[index].lockMoney.ToString();

        UpdateUnLock();
    }

    public void UpdateUnLock()
    {
        currentRole = DataMgr.Instance.roleInfos[index];

        if (currentRole.lockMoney > 0 && !DataMgr.Instance.playerData.buyHero.Contains(index))
        {
            btnUnLock.gameObject.SetActive(true);
            //显示解锁用到的金钱数量
            txtLockMoney.text = currentRole.lockMoney.ToString();

            btnStart.gameObject.SetActive(false);
        }
        else
        {
            btnStart.gameObject.SetActive(true);

            btnUnLock.gameObject.SetActive(false);
        }
        
    }
}
