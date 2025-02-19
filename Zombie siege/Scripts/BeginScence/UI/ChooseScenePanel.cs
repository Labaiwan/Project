using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseScenePanel : BasePanel
{
    public Image imgScene;
    public Text txtTips;

    public Button btnLeft;
    public Button btnRight;

    public Button btnStart;
    public Button btnBack;

    private int index = 0;
    private SceneInfo currentSceneInfo = DataMgr.Instance.sceneInfos[0];
    private int sceneNum = 3;//场景数量
    protected override void Init()
    {
        btnLeft.onClick.AddListener(()=>
        {
            index = (index - 1 + sceneNum) % sceneNum;
            UpdateSceneInfo();
        });

        btnRight.onClick.AddListener(()=>
        {
            index = (index + 1) % sceneNum;
            UpdateSceneInfo();
        });

        btnStart.onClick.AddListener(() =>
        {
            //隐藏面板
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //跳转到游戏场景
            AsyncOperation os = SceneManager.LoadSceneAsync(currentSceneInfo.sceneName);
            os.completed += (obj)=>
            {
                LevelManager.Instance.Init();//场景的初始化
            };

        });

        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //显示选择角色界面
            UIManager.Instance.ShowPanel<ChoosePanel>();
        });

        UpdateSceneInfo();
    }


    private void UpdateSceneInfo()
    {
        currentSceneInfo = DataMgr.Instance.sceneInfos[index];
        //更新画面
        imgScene.sprite = Resources.Load<Sprite>(currentSceneInfo.imgRes);
        //跟新描述
        string tips = "名字:\n" + currentSceneInfo.name + "\n" + "tips:\n" + currentSceneInfo.tips;
        txtTips.text = tips;

    }
}
