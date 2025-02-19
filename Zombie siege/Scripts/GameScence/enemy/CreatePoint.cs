using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePoint : MonoBehaviour
{

    public int enemyNum = 10;//敌人生成数量
    private int nowNum;
    // Start is called before the first frame update
    void Start()
    {
        nowNum = enemyNum;
        Invoke("CreateEnemy",2);
        LevelManager.Instance.UpdateCreateNum(enemyNum);//更新场景还有多少怪物要生成
    }

    public void CreateEnemy()
    {
        int index = Random.Range(0,2);//随机生成 敌人的类型
        MonsterInfo monsterInfo = DataMgr.Instance.monsterInfos[index];
        GameObject enemy = Instantiate(Resources.Load<GameObject>(monsterInfo.res),this.transform.position,this.transform.rotation);
        EnemyObject enemyObject = enemy.GetComponent<EnemyObject>();
        enemyObject.Init(monsterInfo);
        nowNum--;
        if (nowNum > 0)
        {
            Invoke("CreateEnemy", 2);
        }
        LevelManager.Instance.UpdateCreateNum(-1);//更新场景还有多少怪物要生成
        LevelManager.Instance.UpdateliveNum(1);//更新场景存活的怪物
    }

    public bool IsCreateOver()
    {
        return nowNum == 0;
    }
}
