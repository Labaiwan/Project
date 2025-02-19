using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraAnim : MonoBehaviour
{
    private Animator anim;

    private UnityAction afterAnim;//播放完动画要做的事情
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }
    public void TurnLeft(UnityAction action = null)
    {
        anim.SetTrigger("Change2Left");
        afterAnim = action;
    }
    public void TurnRight(UnityAction action = null)
    {
        anim.SetTrigger("Change2Right");
        afterAnim = action;
    }

    public void PlayerOver()
    {
        afterAnim?.Invoke();
        afterAnim = null;
    }
}
