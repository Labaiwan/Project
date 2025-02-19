using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePoint : MonoBehaviour
{

    public int enemyNum = 10;//������������
    private int nowNum;
    // Start is called before the first frame update
    void Start()
    {
        nowNum = enemyNum;
        Invoke("CreateEnemy",2);
        LevelManager.Instance.UpdateCreateNum(enemyNum);//���³������ж��ٹ���Ҫ����
    }

    public void CreateEnemy()
    {
        int index = Random.Range(0,2);//������� ���˵�����
        MonsterInfo monsterInfo = DataMgr.Instance.monsterInfos[index];
        GameObject enemy = Instantiate(Resources.Load<GameObject>(monsterInfo.res),this.transform.position,this.transform.rotation);
        EnemyObject enemyObject = enemy.GetComponent<EnemyObject>();
        enemyObject.Init(monsterInfo);
        nowNum--;
        if (nowNum > 0)
        {
            Invoke("CreateEnemy", 2);
        }
        LevelManager.Instance.UpdateCreateNum(-1);//���³������ж��ٹ���Ҫ����
        LevelManager.Instance.UpdateliveNum(1);//���³������Ĺ���
    }

    public bool IsCreateOver()
    {
        return nowNum == 0;
    }
}
