using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr
{
    private static DataMgr instance = new DataMgr();
    public static DataMgr Instance => instance;

    public musicData music;//音乐数据

    public List<RoleInfo> roleInfos;//角色信息 (只读取)

    public PlayerData playerData;//玩家信息

    public RoleInfo CurrentPlayer;//记录当前玩家角色

    public List<SceneInfo> sceneInfos;//场景信息

    public List<MonsterInfo> monsterInfos;//怪物信息
    private DataMgr()
    {
        Debug.Log(Application.persistentDataPath);
        music = JsonMgr.Instance.LoadData<musicData>("musicData");
        roleInfos = JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        playerData = JsonMgr.Instance.LoadData<PlayerData>("playerData");
        sceneInfos = JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        monsterInfos = JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");

        Debug.Log("读取测试");
        Debug.Log(roleInfos[1].atk);

    }

    public void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(music, "musicData");
    }

    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData,"playerData");
    }
}
