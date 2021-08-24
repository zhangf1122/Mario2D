using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAreaTrigger : MonoBehaviour
{
    public GameObject WinUI;
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
        if (collision.collider.tag == "Player")
        {
            WinUI.SetActive(true);
            ScoreText.ScoreCount = 0;
            GoldText.GoldCount = 0;
            Time.timeScale = 0.0f;
        }
    }
}
