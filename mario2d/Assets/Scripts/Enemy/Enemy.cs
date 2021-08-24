using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator EnemyGroundAnim;
    private Transform EnemyGroundTransform;
    private Rigidbody2D EnemyGroundRigid;
    public float MoveSpeed = 0.5f;
    public Transform checkPointTrans;//获取监测点
    public float checkDistance;//获取监测距离
    public LayerMask EnemyWallMask;//选择要监测障碍物的层
    public LayerMask otherEnemyMask;//选择要监测其他敌人的层
    public int EnemyGroundDieHP = 1;
    public float PlayerSecondJumpSpeed = 2.0f;
    private Transform PlayerTransform;
    public float radius;//锁敌半径

    // Start is called before the first frame update
    void Start()
    {
        EnemyGroundAnim = this.GetComponent<Animator>();
        EnemyGroundTransform = this.GetComponent<Transform>();
        EnemyGroundRigid = this.GetComponent<Rigidbody2D>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //如果死亡，没有任何判定
        if (EnemyGroundDieHP <= 0)
        {
            return;
        }
        //玩家接近时开始移动
        if (PlayerTransform != null)
        {
            float distance = (transform.position - PlayerTransform.position).sqrMagnitude;
            if (distance <= radius)
            {
                EnemyMove();
                CheckObstacle();
                CheckOtherEnemy();
            }
            if (distance > 100)
            {
                Destroy(gameObject);
            }
        }
    }

    private float EnemyGroundDeathDealy = 1.0f;
    private Vector3 EnemyGroundDeathDir = new Vector3(0, 0.02f, 0);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EnemyGroundDieHP --;
            if (EnemyGroundDieHP <= 0)
            {
                BGM.Instance.PlaySound("踩敌人");
                EnemyGroundAnim.SetBool("IsEnemyGroundDie", true);
                //死亡时下移使死亡看起来更正常
                EnemyGroundTransform.position -= EnemyGroundDeathDir;
                collision.GetComponent<Rigidbody2D>().velocity=new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x, PlayerSecondJumpSpeed);
                foreach (Collider2D colliderOfGround in GetComponents<Collider2D>())
                {
                    Destroy(colliderOfGround);
                }
                Destroy(EnemyGroundRigid);
                ScoreText.ScoreCount += 100;
                Destroy(this.gameObject, EnemyGroundDeathDealy);
            }
        }
    }

    public Transform CheckPoint;//设置一个跳跃监测点   
    public float CheckRadius;//设置一个跳跃监测半径   
    public LayerMask WhatIsGround;//设置一个跳跃监测层---角色与地面的检测
    private bool isGround;//判定角色是否着地
    public Vector3 dir = new Vector3(1, 0, 0);
    /// <summary>
    /// 地面敌人移动
    /// </summary>
    private void EnemyMove()
    {
        EnemyGroundTransform.position += dir * Time.deltaTime * MoveSpeed;//向指定方向移动
        isGround = Physics2D.OverlapCircle(CheckPoint.position, CheckRadius, WhatIsGround);
        if (!isGround)
        {
            EnemyGroundRigid.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezeRotation;
        }
        else if (isGround)
        {
            EnemyGroundRigid.constraints = RigidbodyConstraints2D.FreezeAll;
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
    /// 监测是否有其他敌人
    /// </summary>
    public void CheckOtherEnemy()
    {
        Vector2 CheckPos = checkPointTrans.position;
        RaycastHit2D otherEnemyHit = Physics2D.Raycast(CheckPos, checkDir, checkDistance, otherEnemyMask);
        if (otherEnemyHit.collider != null)
        {
            if (otherEnemyHit.collider.CompareTag("EnemyGround") || otherEnemyHit.collider.CompareTag("Mushroom"))
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
    /// 敌人图片(模型)翻转
    /// </summary>
    private void Reverse()
    {
        
        Vector3 scale = EnemyGroundTransform.localScale;
        scale.x *= -1;
        EnemyGroundTransform.localScale = scale;
    }
}
