using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnQuit;

    protected override void Init()
    {
        btnStart.onClick.AddListener(()=>
        {
            //播放摄像机动画 左转
            Camera.main.GetComponent<CameraAnim>().TurnLeft(()=>
            {
                UIManager.Instance.ShowPanel<ChoosePanel>();
            });
            //隐藏开始界面 更新到选择角色界面
            UIManager.Instance.HidePanel<BeginPanel>();
            
        });

        btnSetting.onClick.AddListener(()=>
        {
            //显示设置界面
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        btnQuit.onClick.AddListener(()=>
        {
            //退出游戏
            Application.Quit();
        });
    }
}
