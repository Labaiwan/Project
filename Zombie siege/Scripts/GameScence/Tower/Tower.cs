using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private static Tower instance;
    public static Tower Instace => instance;//���ֵ��������ų����л������� ����Ҫ�������

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
    /// ���������˵ĺ���
    /// </summary>
    /// <param name="hp">�۵���Ѫ��</param>
    public void Damage(int hp)
    {
        if (Dead) return;
        this.hp -= hp;
        if (this.hp <= 0)
        {
            this.hp = 0;
            Dead = true;
            UIManager.Instance.ShowPanel<GameOverPanel>().UpdateInfo(200, false);//ʤ��
        }
        UIManager.Instance.GetPanel<GamePanel>().UpdaHpInfo(this.hp, maxHp);
    }
    private void OnDestroy()
    {
        instance = null;//��ֹ������ �����Ѿ����ٵ�ʵ��
    }
}
