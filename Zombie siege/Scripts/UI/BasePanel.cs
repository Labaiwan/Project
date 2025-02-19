using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public abstract class BasePanel : MonoBehaviour
{
    //控制面板的透明度
    private CanvasGroup canvasGroup = null;
    //透明度渐变速度
    private float speed = 1f;
    //控制是否为显示还是隐藏
    private bool IsShow = false;

    //面板隐藏之后要做的事情
    private UnityAction callback_doAfaterHide = null;
    //获取组件
    protected virtual void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
        }
    }
    
    protected virtual void Start()
    {
        Init();
    }

    //所有面板初始化的地方
    protected abstract void Init();

    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;

        IsShow = true;
    }

    //callback是Hide之后要做的事情
    public virtual void HideMe(UnityAction callback)
    {
        canvasGroup.alpha = 1;
        IsShow = false;

        callback_doAfaterHide = callback;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += speed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        else if (!IsShow && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= speed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                if (callback_doAfaterHide != null)
                {
                    callback_doAfaterHide();
                }
            }
        }
    }
}
