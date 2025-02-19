using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    //单例模式
    private static UIManager instance = new UIManager();

    public static UIManager Instance => instance;

    //用字典存储面板
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform canvasTrans;

    private UIManager()
    {
        //canvas动态生成
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/canvas"));
        canvasTrans = canvas.transform;
        //过场景不移出
        GameObject.DontDestroyOnLoad(canvas);
        
    }

    //显示面板
    public T ShowPanel<T>() where T : BasePanel
    {
        //规定类名和面板资源的名字致
        string panelName = typeof(T).Name;

        //存在直接返回出去
        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }

        //根据面板名字 动态加载面板
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/"+panelName));
        panelObj.transform.SetParent(canvasTrans,false);

        //存储这个面板
        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName, panel);

        //调用过渡显示面板程序
        panel.ShowMe();

        return panel;

    }


    //隐藏面板
    public void HidePanel<T>(bool isFade = true)
    {
        //根据泛型获取名字
        string panelName = typeof(T).Name;

        //判断当前要隐藏的面板是否存在
        if (panelDic.ContainsKey(panelName))
        {
            //淡出面板后 销毁面板
            if (isFade)
            {
                panelDic[panelName].HideMe(()=>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else//直接销毁面板
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }


    }
    //获取面板
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;

        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }

        return null;
    }
}
