using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 方块的状态
/// </summary>
public class RockState : MonoBehaviour
{
    public GameObject Rock;
    public GameObject[] rocks;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && collision.contacts[0].normal == Vector2.up)
        {
            BGM.Instance.PlaySound("顶破砖");
            foreach(GameObject rock in rocks)
            {
                rock.transform.parent = null;
                //给四个小方块添加刚体
                Rigidbody2D rocksBody = rock.AddComponent<Rigidbody2D>();
                Vector2 rocksDir = rock.transform.position - transform.position;
                rocksBody.AddForce(rocksDir * 1000f);
                //四散后一秒销毁
                Destroy(rock, 1f);
            }
            Destroy(gameObject);
        }
    }
}
