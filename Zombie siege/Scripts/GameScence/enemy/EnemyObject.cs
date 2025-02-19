using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyObject : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    private MonsterInfo enemy = null;//记录相关属性
    private int currentHp = 0;//记录自己的血量
    private bool dead = false;

    private float lastAtkTime = 0;
    // Start is called before the first frame update
    void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
        agent = this.gameObject.GetComponent<NavMeshAgent>();
    }
    public void Init(MonsterInfo enemyinfo)
    {
        enemy = enemyinfo;
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(enemyinfo.animator);
        currentHp = enemy.hp;
        agent.speed = agent.acceleration = enemyinfo.moveSpeed;
        agent.angularSpeed = enemyinfo.roundSpeed;
    }

    //出生后跑到目标塔的位置
    public void BornOver()
    {
        agent.destination = Tower.Instace.transform.position;
        anim.SetBool("run",true);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("run", agent.velocity!=Vector3.zero);

        if (Vector3.Distance(this.transform.position, Tower.Instace.transform.position) < 5
            && Time.time > lastAtkTime + enemy.atkOffst)
        {
            anim.SetTrigger("attack");
            lastAtkTime = Time.time;
        }
        
    }

    //攻击后 塔会掉血(动画过程中 进行范围检测)
    public void  AtkEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up, 1, 1 << LayerMask.NameToLayer("Tower"));

        if (colliders != null)
        {
            Tower.Instace.Damage(enemy.atk);
        }
    }

    //受伤
    public void Damege(int hp)
    {
        if (dead)
            return;
        this.currentHp -= hp;
        anim.SetTrigger("damage");
        if (this.currentHp < 0)
        {
            dead = true;
            anim.SetBool("dead",true);
        }
    }

    //死亡动画事件
    public void DeadEvent()
    {
        //销毁物体 由游戏管理类处理暂时不处理
        Destroy(this.gameObject);
        LevelManager.Instance.UpdateliveNum(-1);
        //判断游戏是否结束
        if (LevelManager.Instance.IsOver())
        {
            UIManager.Instance.ShowPanel<GameOverPanel>().UpdateInfo(200,true);//胜利
        }
        
    }
}
