using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    private static LevelManager instance = new LevelManager();

    public static LevelManager Instance => instance;

    private Transform createPos;

    private int CreateNum = 0;//还要生成的数量
    private int liveNum = 0;//敌人存活的数量


    //初始化场景 这里就不加参数了
    public void Init()
    {
        //加载角色和界面 设置摄像头
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
    //用来判断游戏是否结束
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
