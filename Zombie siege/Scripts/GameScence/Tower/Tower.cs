using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private static Tower instance;
    public static Tower Instace => instance;//这种单例会随着场景切换而销毁 所以要主动清空

    private int hp = 100;
    private int maxHp = 100;

    public bool Dead = false;

    void Start()
    {
        instance = this;
    }

    public void UpdateHp()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdaHpInfo(hp,maxHp);
    }

    /// <summary>
    /// 防御塔受伤的函数
    /// </summary>
    /// <param name="hp">扣掉的血量</param>
    public void Damage(int hp)
    {
        if (Dead) return;
        this.hp -= hp;
        if (this.hp <= 0)
        {
            this.hp = 0;
            Dead = true;
            UIManager.Instance.ShowPanel<GameOverPanel>().UpdateInfo(200, false);//胜利
        }
        UIManager.Instance.GetPanel<GamePanel>().UpdaHpInfo(this.hp, maxHp);
    }
    private void OnDestroy()
    {
        instance = null;//防止过场景 引用已经销毁的实例
    }
}
