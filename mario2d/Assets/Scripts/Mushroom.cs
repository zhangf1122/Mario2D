using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制蘑菇的生成以及后续移动的脚本
/// </summary>
public class Mushroom : MonoBehaviour
{
    private Transform MushroomTransform;
    private Rigidbody2D MushroomRigid;
    public float MoveSpeed;//设置移动速度
    public Transform checkPointTrans;//获取监测点
    public float checkDistance;//获取监测距离
    public LayerMask EnemyWallMask;//选择要监测障碍物的层
    public LayerMask otherEnemyMask;//选择要监测敌人的层

    void Start()
    {
        MushroomTransform = this.GetComponent<Transform>();
        MushroomRigid = this.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        MushroomMove();
        CheckObstacle();
        CheckOtherEnemy();
    }

    public Transform CheckPoint;//设置一个跳跃监测点   
    public float CheckRadius;//设置一个跳跃监测半径   
    public LayerMask WhatIsGround;//设置一个跳跃监测层---角色与地面的检测
    private bool isGround;//判定角色是否着地
    public Vector3 dir = new Vector3(1, 0, 0);
    /// <summary>
    /// 蘑菇移动
    /// </summary>
    private void MushroomMove()
    {
        MushroomTransform.position += dir * Time.deltaTime * MoveSpeed;//向指定方向移动
        isGround = Physics2D.OverlapCircle(CheckPoint.position, CheckRadius, WhatIsGround);
        if (!isGround)
        {
            MushroomRigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (isGround)
        {
            MushroomRigid.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public Vector2 checkDir = new Vector2(1, 0);
    /// <summary>
    /// 监测是否有障碍物
    /// </summary>
    public void CheckObstacle()
    {
        Vector2 CheckPos = checkPointTrans.position;
        RaycastHit2D ObstacleHit = Physics2D.Raycast(CheckPos, checkDir, checkDistance, EnemyWallMask);
        if (ObstacleHit.collider != null)
        {
            ChangeEnemyMoveDir();
        }
    }

    /// <summary>
    /// 监测是否有敌人
    /// </summary>
    public void CheckOtherEnemy()
    {
        Vector2 CheckPos = checkPointTrans.position;
        RaycastHit2D otherEnemyHit = Physics2D.Raycast(CheckPos, checkDir, checkDistance, otherEnemyMask);
        if (otherEnemyHit.collider != null)
        {
            if (otherEnemyHit.collider.CompareTag("EnemyGround")|| otherEnemyHit.collider.CompareTag("Mushroom"))
            {
                ChangeEnemyMoveDir();
            }
        }
    }

    /// <summary>
    /// 改变移动方向
    /// </summary>
    public void ChangeEnemyMoveDir()
    {
        dir.x *= -1;//移动方向改变
        checkDir *= -1;//射线检测方向改变
        Reverse();
    }

    /// <summary>
    /// 蘑菇图片(模型)翻转
    /// </summary>
    private void Reverse()
    {

        Vector3 scale = MushroomTransform.localScale;
        scale.x *= -1;
        MushroomTransform.localScale = scale;
    }
}
