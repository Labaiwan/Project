using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    private static LevelManager instance = new LevelManager();

    public static LevelManager Instance => instance;

    private Transform createPos;

    private int CreateNum = 0;//��Ҫ���ɵ�����
    private int liveNum = 0;//���˴�������


    //��ʼ������ ����Ͳ��Ӳ�����
    public void Init()
    {
        //���ؽ�ɫ�ͽ��� ��������ͷ
        UIManager.Instance.ShowPanel<GamePanel>();

        createPos = GameObject.Find("plaerInitPos").transform;
        RoleInfo currentPlayer = DataMgr.Instance.CurrentPlayer;
        GameObject player = GameObject.Instantiate(Resources.Load<GameObject>(currentPlayer.res), createPos.position,createPos.rotation);

        Camera.main.GetComponent<CameraFllow>().SetPlayerPos(player.transform);
    }

    public void UpdateCreateNum(int num)
    {
        CreateNum += num;
    }

    public void UpdateliveNum(int num)
    {
        liveNum += num;
    }
    //�����ж���Ϸ�Ƿ����
    public bool IsOver()
    {
        if (CreateNum == 0 && liveNum == 0)
        {
            return true;
        }
        return false;
    }

    public void clear()
    {
        CreateNum = 0;
        liveNum = 0;
    }
}
