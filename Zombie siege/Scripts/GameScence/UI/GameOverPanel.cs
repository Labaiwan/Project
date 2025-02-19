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
            //��ӽ�Ǯ��
            DataMgr.Instance.playerData.money += money;
            DataMgr.Instance.SavePlayerData(); 
            //��������������
            LevelManager.Instance.clear();
        });

        //ȡ���������
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// ���½���������ʾ
    /// </summary>
    /// <param name="money">�����һ�ȡ�Ľ�Ǯ����</param>
    /// <param name="isWin">�Ƿ�ʤ��</param>
    public void UpdateInfo(int money,bool isWin)
    {
        //�ر���Ϸ����
        UIManager.Instance.HidePanel<GamePanel>();
        txtEndInfo.text = isWin ? "��Ϸʤ��" :"��Ϸ����";
        txtMoney.text = money.ToString();
        this.money = money;
    }

}
