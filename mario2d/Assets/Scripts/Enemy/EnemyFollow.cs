using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform EnemyFollowTransform;
    public float moveSpeed = 0.5f;
    public float StartWaitTime = 3.0f;
    private float waitTime;
    public Transform movePos;
    public Transform OriginalPos;
    public Transform ChangePos;
    // Start is called before the first frame update
    void Start()
    {
        EnemyFollowTransform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    //移动
    private void EnemyMove()
    {
        if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
        {
            if (waitTime <= 0)
            {
                if (movePos.position == ChangePos.position)
                {
                    movePos.position = OriginalPos.position;
                }
                else if (movePos.position == OriginalPos.position)
                {
                    movePos.position = ChangePos.position;
                }
                waitTime = StartWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        //移动
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, movePos.position, moveSpeed * Time.deltaTime);//移动
        }
    }
}
