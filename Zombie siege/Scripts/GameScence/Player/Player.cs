using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public float rotateSpeed = 50;//控制旋转速度

    //游戏中的金钱数量
    private int money = 200;

    //射击起始位置
    private Transform shootPos;

    //记录角色信息
    //private RoleInfo playerInfo = DataMgr.Instance.CurrentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //UpdateMoneyShow();
        shootPos = CheckChild(this.transform, "shootPosition");
    }

    //递归找到子物体
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

        //通过按下左shift键下蹲
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

    //更新金钱显示
    void UpdateMoneyShow()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }

    /// <summary>
    /// 游戏中花掉的钱
    /// </summary>
    /// <param name="money">花掉的金钱数量</param>
    void ChangeMoney(int money)
    {
        if (this.money < money)
            return;
        this.money -= money;
        UpdateMoneyShow();
    }

    //使用刀攻击的事件函数
    void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("Monster"));
        RoleInfo playerInfo = DataMgr.Instance.CurrentPlayer;
        for (int i = 0; i < colliders.Length; i++)
        {
            
            //怪物减去血量的函数
            colliders[i].gameObject.GetComponent<EnemyObject>().Damege(playerInfo.atk);
            break;
        }
    }

    //使用枪攻击的事件函数
    void ShootEvent()
    {
        RaycastHit[] hits = Physics.RaycastAll(shootPos.position,shootPos.forward,1000,1 << LayerMask.NameToLayer("Monster"));
        RoleInfo playerInfo = DataMgr.Instance.CurrentPlayer;
        for (int i = 0; i < hits.Length; i++)
        {
            //怪物减去血量的函数
            hits[i].collider.gameObject.GetComponent<EnemyObject>().Damege(playerInfo.atk);
            break;
        }
    }
}
