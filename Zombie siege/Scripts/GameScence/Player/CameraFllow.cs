using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour
{
    public Transform player;
    private Vector3 targetPos;//摄像机目标位置

    public float height = 3;//摄像头距离角色得到高度
    public float back = 3;//摄像头距离角色背后的距离
    public float ViscousForce = 5;//摄像头移动的阻力
    public float rootOffset = 0;

    public float roatetSpeed = 5;//旋转速度
    private Quaternion targetQua;//摄像机目标旋转角度


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            targetPos = player.position + new Vector3(0, height, 0) - player.forward * back;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, ViscousForce * Time.deltaTime);
            Camera.main.transform.LookAt(player.position - new Vector3(0, rootOffset, 0));
        }
        //targetQua = Quaternion.LookRotation(targetPos + Vector3.up* rootOffset-this.transform.position);
        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetQua, roatetSpeed*Time.deltaTime);
    }

    public void SetPlayerPos(Transform pos)
    {
        player = pos;
    }
}
