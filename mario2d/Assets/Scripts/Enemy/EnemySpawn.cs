using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人生成器
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    public GameObject Enemy;
    protected Transform m_transform;
    private Transform PlayerTransform;
    public float radius;//锁敌半径
    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerTransform != null)
        {
            float distance = (transform.position - PlayerTransform.position).sqrMagnitude;
            if (distance <= radius)
                EnemyGroundSpawn();
        }
    }

    void EnemyGroundSpawn()
    {
        Instantiate(Enemy, m_transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
