using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnClose;
    public Toggle togMusic;
    public Toggle togSound;
    public Slider sliderMusic;
    public Slider sliderSound;

    protected override void Init()
    {
        //读取json数据进行UI的显示更新
        musicData m = DataMgr.Instance.music;

        togMusic.isOn = m.isOpenMusic;
        togSound.isOn = m.isOPenSound;
        sliderMusic.value = m.musicVolume;
        sliderSound.value = m.soundVolume;

        //UI控件的监听事件
        btnClose.onClick.AddListener(()=>
        {
            //隐藏面板
            UIManager.Instance.HidePanel<SettingPanel>();
            //存储数据
            DataMgr.Instance.SaveMusicData();
        });

        togMusic.onValueChanged.AddListener((v)=>
        {
            //设置背景音乐是否开启
            BkMusic.Instance.SetIsOpen(v);
            //修改背景音乐数据
            DataMgr.Instance.music.isOpenMusic = v;
        });

        togSound.onValueChanged.AddListener((v) =>
        {
            //修改音效数据
            DataMgr.Instance.music.isOPenSound = v;
        });

        sliderMusic.onValueChanged.AddListener((v)=>
        {
            //设置背景音乐音量
            BkMusic.Instance.SetVolume(v);
            //修改对应数据
            DataMgr.Instance.music.musicVolume = v;
        });

        sliderSound.onValueChanged.AddListener((v) =>
        {
            //修改对应数据
            DataMgr.Instance.music.soundVolume = v;
        });
    }
}
