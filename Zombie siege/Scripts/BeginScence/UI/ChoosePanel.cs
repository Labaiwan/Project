using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChoosePanel : BasePanel
{
    public Text txtMoney;//ӵ�еĽ�Ǯ����
    public Text txtTips;//��������

    public Button btnLeft;
    public Button btnRight;

    public Button btnStart;
    public Button btnBack;

    public Button btnUnLock;
    public Text txtLockMoney;

    private Transform heroPos;

    private GameObject player = null;
    private int index = 0;//��ǰ��ʾ��ɫ������
    private int playerNum = 3;//��ɫ������
    private RoleInfo currentRole = null;//��ǰ��ɫ

    protected override void Init()
    {
        //��ʾ��Ǯ����
        txtMoney.text = DataMgr.Instance.playerData.money.ToString();
        //��ȡ����ɫ���ɵ�λ��
        heroPos = GameObject.Find("heroPos").transform;

        UpdateRole();

        //��ť�¼�����
        btnLeft.onClick.AddListener(()=>
        {
            //��ǰ��ҳ ѡ������
            index = (index - 1+ playerNum)% playerNum;
            //�����������½�ɫ
            UpdateRole();
        });


        btnRight.onClick.AddListener(()=>
        {
            //���ҳ ѡ������
            index = (index + 1) % playerNum;
            //�����������½�ɫ
            UpdateRole();

        });

        btnStart.onClick.AddListener(()=>
        {
            //��¼ѡ��õĽ�ɫ
            DataMgr.Instance.CurrentPlayer = DataMgr.Instance.roleInfos[index];
            //���ص�ǰ��� ��ʾѡ�񳡾����
            UIManager.Instance.HidePanel<ChoosePanel>();
            UIManager.Instance.ShowPanel<ChooseScenePanel>();
        });

        btnBack.onClick.AddListener(()=>
        {
            //������壨ɾ���Ѿ������õĽ�ɫ��
            UIManager.Instance.HidePanel<ChoosePanel>();

            //����ͷ��ת ��ʾ��ʼ���
            //������������� ��ת
            Camera.main.GetComponent<CameraAnim>().TurnRight(() =>
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
                //ɾ����֮ǰ�Ľ�ɫ
                if (player != null)
                {
                    DestroyImmediate(player);
                    player = null;
                }
            });
            
        });

        btnUnLock.onClick.AddListener(()=>{

            currentRole = DataMgr.Instance.roleInfos[index];

            //�ж��Ƿ����㹺������
            if (DataMgr.Instance.playerData.money > currentRole.lockMoney)
            {
                DataMgr.Instance.playerData.money -= currentRole.lockMoney;
                txtMoney.text = DataMgr.Instance.playerData.money.ToString();
                DataMgr.Instance.playerData.buyHero.Add(index);

                //�����������
                DataMgr.Instance.SavePlayerData();
                UpdateUnLock();
                //��ʾ����ɹ�
                UIManager.Instance.ShowPanel<TipPanel>().SetContent("����ɹ���");

            }
            else
            {
                //��ʾ����ʧ��
                TipPanel tipPanel = UIManager.Instance.ShowPanel<TipPanel>();
                tipPanel.SetContent("��Ǯ��������");
            }
        });
    }

    public void UpdateRole()
    {
        //����ǰɾ����֮ǰ�Ľ�ɫ
        if (player != null)
        {
            Destroy(player);
            player = null;
        } 
        //��ʾ��ɫ
        player = GameObject.Instantiate(Resources.Load<GameObject>(DataMgr.Instance.roleInfos[index].res), heroPos.position,heroPos.rotation);
        Destroy(player.GetComponent("Player"));
        //����λ��
        /*player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
        player.transform.localScale = Vector3.one;*/
        //��ʾ��ɫ������
        txtTips.text = DataMgr.Instance.roleInfos[index].tips;
        //��ʾ��ɫ�Ľ��������Ǯ
        txtLockMoney.text = DataMgr.Instance.roleInfos[index].lockMoney.ToString();

        UpdateUnLock();
    }

    public void UpdateUnLock()
    {
        currentRole = DataMgr.Instance.roleInfos[index];

        if (currentRole.lockMoney > 0 && !DataMgr.Instance.playerData.buyHero.Contains(index))
        {
            btnUnLock.gameObject.SetActive(true);
            //��ʾ�����õ��Ľ�Ǯ����
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
