using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 镜头跟随
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform target;//选择要跟随的物体（这里是玩家）
    public float lerpSpeed = 3.0f;//镜头移动速度
    public Vector2 minPosition;//能移动的最小位置坐标
    public Vector2 maxPosition;//能移动的最大位置坐标
    private void Start()
    {

    }

    private void LateUpdate()
    {
        if (target != null)
        {
            if (transform.position != target.position)
            {
                Vector3 targetPos = target.position;//物体坐标
                targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);//限定能移动的x轴范围
                targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);//限定能移动的y轴范围
                //transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);//用Lerp函数实现相机平滑的移动
                transform.position = targetPos;
            }
        }
    }
}

