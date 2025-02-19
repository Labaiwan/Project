using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow : MonoBehaviour
{
    public Transform player;
    private Vector3 targetPos;//�����Ŀ��λ��

    public float height = 3;//����ͷ�����ɫ�õ��߶�
    public float back = 3;//����ͷ�����ɫ����ľ���
    public float ViscousForce = 5;//����ͷ�ƶ�������
    public float rootOffset = 0;

    public float roatetSpeed = 5;//��ת�ٶ�
    private Quaternion targetQua;//�����Ŀ����ת�Ƕ�


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
