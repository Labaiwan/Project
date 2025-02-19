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
    public GameObject wapen;//��������ʾ�����ظ�����

    public float hpWidth = 800;//Ѫ���ĳ���

    public List<weaponPanel> weapons;//������ʾ�б�

    protected override void Init()
    {
        btnBack.onClick.AddListener(() => {
            //�ص���ʼ����
            SceneManager.LoadScene("BeginScene");
        });
        //�������
        Cursor.lockState = CursorLockMode.Locked;
        //�����·�����������
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
