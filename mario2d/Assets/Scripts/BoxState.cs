using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxState : MonoBehaviour
{
    public int BoxHP = 5;
    public GameObject Go;
    public string soundOfGo;
    private Animator BoxAnim;
    public Transform MushroomTransform;
    // Start is called before the first frame update
    void Start()
    {
        BoxAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && collision.contacts[0].normal == Vector2.up)
        {
            if (BoxHP > 0)
            {
                BoxHP--;
                //播放顶出物品的声音
                BGM.Instance.PlaySound(soundOfGo);
                //将顶出物体实例化
                if (Go.tag == "Mushroom")
                {
                    Instantiate(Go, MushroomTransform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Go, transform.position, Quaternion.identity);
                }
                if (BoxHP <= 0)
                {
                    BoxAnim.SetBool("IsBoxDie",true);
                }
            }
            else
            {
                BGM.Instance.PlaySound("顶砖石块");
            }
        }
    }
}
