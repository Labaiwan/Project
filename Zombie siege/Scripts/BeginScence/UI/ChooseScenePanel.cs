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
    private int sceneNum = 3;//��������
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
            //�������
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //��ת����Ϸ����
            AsyncOperation os = SceneManager.LoadSceneAsync(currentSceneInfo.sceneName);
            os.completed += (obj)=>
            {
                LevelManager.Instance.Init();//�����ĳ�ʼ��
            };

        });

        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChooseScenePanel>();
            //��ʾѡ���ɫ����
            UIManager.Instance.ShowPanel<ChoosePanel>();
        });

        UpdateSceneInfo();
    }


    private void UpdateSceneInfo()
    {
        currentSceneInfo = DataMgr.Instance.sceneInfos[index];
        //���»���
        imgScene.sprite = Resources.Load<Sprite>(currentSceneInfo.imgRes);
        //��������
        string tips = "����:\n" + currentSceneInfo.name + "\n" + "tips:\n" + currentSceneInfo.tips;
        txtTips.text = tips;

    }
}
