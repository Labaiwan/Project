using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkMusic : MonoBehaviour
{
    private static BkMusic instance = null;

    public static BkMusic Instance => instance;


    private AudioSource bkAudio;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        bkAudio = this.GetComponent<AudioSource>();

        musicData m = DataMgr.Instance.music;
        SetIsOpen(m.isOpenMusic);
        SetVolume(m.musicVolume);
    }

    //设置是否打开
    public void SetIsOpen(bool IsOpen)
    {
        bkAudio.mute = !IsOpen;
    }

    //设置背景音乐大小
    public void SetVolume(float value)//0 - 1
    {
        bkAudio.volume = value;
    }
}
