using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel
{
    public Text txtEndInfo;
    public Text txtMoney;
    public Button btnClose;

    private int money;
    
    protected override void Init()
    {
        btnClose.onClick.AddListener(()=>
        {
            UIManager.Instance.HidePanel<GameOverPanel>();
            
            SceneManager.LoadScene("BeginSceene");
            //添加金钱数
            DataMgr.Instance.playerData.money += money;
            DataMgr.Instance.SavePlayerData(); 
            //清除场景相关数据
            LevelManager.Instance.clear();
        });

        //取消鼠标锁定
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// 更新结束界面显示
    /// </summary>
    /// <param name="money">最后玩家获取的金钱数量</param>
    /// <param name="isWin">是否胜利</param>
    public void UpdateInfo(int money,bool isWin)
    {
        //关闭游戏界面
        UIManager.Instance.HidePanel<GamePanel>();
        txtEndInfo.text = isWin ? "游戏胜利" :"游戏结束";
        txtMoney.text = money.ToString();
        this.money = money;
    }

}
