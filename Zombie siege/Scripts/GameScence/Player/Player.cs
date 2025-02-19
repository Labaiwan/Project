using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public float rotateSpeed = 50;//������ת�ٶ�

    //��Ϸ�еĽ�Ǯ����
    private int money = 200;

    //�����ʼλ��
    private Transform shootPos;

    //��¼��ɫ��Ϣ
    //private RoleInfo playerInfo = DataMgr.Instance.CurrentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //UpdateMoneyShow();
        shootPos = CheckChild(this.transform, "shootPosition");
    }

    //�ݹ��ҵ�������
    public Transform CheckChild(Transform TF,string childName)
    {
        Transform x = TF.Find(childName);
        if (x != null)
        {
            return x;
        }
        if (TF.childCount == 0)
        {
            return null;
        }
        for (int i = 0; i < TF.childCount; i++)
        {
            Transform childTF = TF.GetChild(i);
            x = CheckChild(childTF, childName);
            if (x != null)
            {
                return x;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        anim.SetFloat("VSpeed", Input.GetAxis("Vertical"));

        this.transform.Rotate(this.transform.up, Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed);

        //ͨ��������shift���¶�
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetLayerWeight(1, 1);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetLayerWeight(1, 0);
        }

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("Roll");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Fire");
        }
    }

    //���½�Ǯ��ʾ
    void UpdateMoneyShow()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }

    /// <summary>
    /// ��Ϸ�л�����Ǯ
    /// </summary>
    /// <param name="money">�����Ľ�Ǯ����</param>
    void ChangeMoney(int money)
    {
        if (this.money < money)
            return;
        this.money -= money;
        UpdateMoneyShow();
    }

    //ʹ�õ��������¼�����
    void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));
        RoleInfo playerInfo = DataMgr.Instance.CurrentPlayer;
        for (int i = 0; i < colliders.Length; i++)
        {
            
            //�����ȥѪ���ĺ���
            colliders[i].gameObject.GetComponent<EnemyObject>().Damege(playerInfo.atk);
            break;
        }
    }

    //ʹ��ǹ�������¼�����
    void ShootEvent()
    {
        RaycastHit[] hits = Physics.RaycastAll(shootPos.position,shootPos.forward,1000,1 << LayerMask.NameToLayer("Monster"));
        RoleInfo playerInfo = DataMgr.Instance.CurrentPlayer;
        for (int i = 0; i < hits.Length; i++)
        {
            //�����ȥѪ���ĺ���
            hits[i].collider.gameObject.GetComponent<EnemyObject>().Damege(playerInfo.atk);
            break;
        }
    }
}
