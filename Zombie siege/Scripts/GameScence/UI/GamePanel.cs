using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GamePanel : BasePanel
{
    public Text txtHpValue;
    public Image imgHp;

    public Text txtWave;
    public Text txtMoney;

    public Button btnBack;
    public GameObject wapen;//武器的显示和隐藏父对象

    public float hpWidth = 800;//血条的长度

    public List<weaponPanel> weapons;//武器显示列表

    protected override void Init()
    {
        btnBack.onClick.AddListener(() => {
            //回到开始场景
            SceneManager.LoadScene("BeginScene");
        });
        //隐藏鼠标
        Cursor.lockState = CursorLockMode.Locked;
        //隐藏下方的武器界面
        wapen.SetActive(false);
    }

    public void UpdaHpInfo(float hp,float maxHp)
    {
        imgHp.rectTransform.sizeDelta = new Vector2((hp/maxHp)*hpWidth,35);

        txtHpValue.text = hp.ToString() + "/" + maxHp.ToString();
    }

    public void UpdateWave(int num,int maxNum)
    {
        txtWave.text = num.ToString() + "/" + maxNum.ToString();
    }

    public void UpdateMoney(int money,int maxMoney=1000)
    {
        txtMoney.text = money.ToString() + "/" + maxMoney.ToString();
    }
}
