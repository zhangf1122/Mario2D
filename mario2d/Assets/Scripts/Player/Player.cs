using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform thisTransform;
    private Rigidbody2D thisRigid;
    private Animator thisAnim;
    private CapsuleCollider2D thisColl;
    private Renderer thisRender;
    public int PlayerHP = 1;
    public int blinks = 4;
    public float time = 1f;
    private bool StateRun = false;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.GetComponent<Transform>();
        thisRigid = this.GetComponent<Rigidbody2D>();
        thisAnim = this.GetComponent<Animator>();
        thisColl = this.GetComponent<CapsuleCollider2D>();
        thisRender = this.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHP <= 0)
        {
            return;
        }
        PlayerMove();
        if (StateRun)
        {
            ChangeMario();
        }
    }

    /// <summary>
    /// 角色移动
    /// </summary>
    public float moveSpeed = 1.0f;
    public float JumpSpeed = 4.0f;
    public Transform CheckPoint;//设置一个小马里奥跳跃监测点
    public Transform BigCheckPoint;//设置一个大马里奥跳跃监测点
    public float CheckRadius;//设置一个跳跃监测半径   
    public LayerMask WhatIsGround;//设置一个跳跃监测层---角色与地面的检测
    private bool isGround;//判定角色是否着地

    private void PlayerMove()
    {
        //左移
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            thisAnim.SetBool("RunState", true);
            //刚体的移动
            thisRigid.velocity = new Vector2(-moveSpeed, thisRigid.velocity.y);
            //图片沿y轴反转
            thisTransform.localScale = new Vector2(-1, 1);
        }
        //右移
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            thisAnim.SetBool("RunState", true);
            thisRigid.velocity = new Vector2(moveSpeed, thisRigid.velocity.y);
            thisTransform.localScale = new Vector2(1, 1);
        }
        //不移动
        else
        {
            thisAnim.SetBool("RunState", false);
            thisRigid.velocity = new Vector2(0, thisRigid.velocity.y);
        }
        //判断是否在地面上
        if (PlayerHP <= 1)
        {
            isGround = Physics2D.OverlapCircle(CheckPoint.position, CheckRadius, WhatIsGround);
        }
        else if (PlayerHP == 2)
        {
            isGround = Physics2D.OverlapCircle(BigCheckPoint.position, CheckRadius, WhatIsGround);
        }
        thisAnim.SetBool("IsGround", isGround);
        //跳跃
        if (Input.GetKey(KeyCode.K)&&isGround)
        {
            //播放跳跃音效
            BGM.Instance.PlaySound("跳");
            thisRigid.velocity = new Vector2(thisRigid.velocity.x, JumpSpeed);
        }
    }

    /*public Transform EnemyCheckPoint;//设置一个敌人监测点
    public float EnemyCheckRadius;
    public LayerMask WhatIsEnemy;
    private bool falling;
    public Transform RockCheckPoint;//设置一个方块监测点
    public float RockCheckRadius;
    public LayerMask WhatIsRock;
    private bool IsRockTouch;*/
    /// <summary>
    /// 角色碰撞
    /// </summary>
    private void PlayerCollision()
    {
        //判断是否踩到敌人
        //falling = Physics2D.OverlapCircle(EnemyCheckPoint.position, EnemyCheckRadius, WhatIsEnemy);
        //thisAnim.SetBool("Falling", falling);

        //判断是否顶撞方块
        //IsRockTouch = Physics2D.OverlapCircle(RockCheckPoint.position, RockCheckRadius, WhatIsRock);
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (thisAnim.GetBool("Falling"))
        {
            if (collision.gameObject.tag == "EnemyGround")
            {
                BGM.Instance.PlaySound("踩敌人");
                Enemy.EnemyGroundDieHP = 0;
                thisRigid.velocity = new Vector2(thisRigid.velocity.x, SecondJumpSpeed);                
            }
        }
    }*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyFollow")&&other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            if (IsInvincible)
            {
                return;
            }
            PlayerHP--;
            if (PlayerHP == 1)
            {
                StateRun = true;
            }
            else if (PlayerHP <= 0)
            {
                PlayerDie(); 
            }
        }
        else if (other.gameObject.CompareTag("Mushroom") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            Destroy(other.gameObject);
            ScoreText.ScoreCount += 200;
            ChangeBigMario();
        }
        else if (other.gameObject.CompareTag("AirArea") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            PlayerHP = 0;
            PlayerDie();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag=="EnemyGround")
        {
            if (IsInvincible)
            {
                return;
            }
            PlayerHP--;
            if (PlayerHP == 1)
            {
                StateRun = true;
            }
            else if (PlayerHP <= 0)
            {
                PlayerDie();
            }
        }
    }

    public float deathDelay = 1.5f;
    /// <summary>
    /// 角色死亡
    /// </summary>
    public void PlayerDie()
    {
        thisAnim.SetBool("Die", true);
        Destroy(this.gameObject, deathDelay);
        BGM.Instance.PauseMusic();
        BGM.Instance.PlaySound("死亡1");
        thisRigid.velocity = new Vector2(0, thisRigid.velocity.y);
        thisRigid.constraints= RigidbodyConstraints2D.FreezeAll;
        Destroy(GetComponent<Collider2D>());
        thisRigid.constraints = ~RigidbodyConstraints2D.FreezePositionY;
        thisRigid.AddForce(Vector2.up * 1000f);
        Invoke("PlayerDeathMusic", 0.5f);
    }

    void PlayerDeathMusic()
    {
        BGM.Instance.PlaySound("死亡2");
    }

    /// <summary>
    /// 变成大玛里奥
    /// </summary>
    void ChangeBigMario()
    {
        PlayerHP++;
        if (PlayerHP >= 2)
        {
            PlayerHP = 2;
        }
        thisAnim.SetBool("MarioBig", true);
        BGM.Instance.PlaySound("吃到蘑菇或花");
        thisColl.size = new Vector2(0.145f, 0.32f);
    }

    /// <summary>
    /// 变回小玛丽奥
    /// </summary>
    void ChangeMario()
    {
        PlayerHP = 1;
        thisAnim.SetBool("MarioBig", false);
        thisColl.size = new Vector2(0.13f, 0.16f);
        IsInvincible = true;
        ChangeInvincible();
    }

    public bool IsInvincible = false;
    public float InvincibleTime = 1.0f;
    /// <summary>
    /// 变小时获得一定量的无敌时间
    /// </summary>
    void ChangeInvincible()
    {
        if (IsInvincible)
        {
            InvincibleTime -= Time.deltaTime;
            BlinkPlayer(blinks, time);
            if (InvincibleTime <= 0)
            {
                IsInvincible = false;
                InvincibleTime = 1;
                StateRun = false;
            }
        }
    }

    void BlinkPlayer(int numBlinks,float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks,float seconds)
    {
        for(int i = 0; i < numBlinks * 2; i++)
        {
            thisRender.enabled = !thisRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        thisRender.enabled = true;
    }
}
