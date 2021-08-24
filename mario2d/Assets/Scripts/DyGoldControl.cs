using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyGoldControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150f);
        GoldText.GoldCount++;
        ScoreText.ScoreCount += 10;
        //销毁金币
        Destroy(gameObject, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
