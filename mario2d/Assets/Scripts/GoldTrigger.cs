using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BGM.Instance.PlaySound("金币");
            GoldText.GoldCount++;
            ScoreText.ScoreCount += 10;
            Destroy(gameObject);
        }
    }
}
