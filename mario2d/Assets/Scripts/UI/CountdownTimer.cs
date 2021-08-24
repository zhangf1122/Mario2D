using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public GameObject text;
    public GameObject GameOverUI;
    public int TotalTime = 30;

    void Start()
    {
        StartCoroutine(TimeRun());
    }

    IEnumerator TimeRun()
    {
        while (TotalTime >= 0)
        {
            text.GetComponent<Text>().text = TotalTime.ToString();
            yield return new WaitForSeconds(1);
            TotalTime--;
            if (TotalTime <= 0)
            {
                Time.timeScale = 0.0f;
                BGM.Instance.PauseMusic();
                ScoreText.ScoreCount = 0;
                GoldText.GoldCount = 0;
                GameOverUI.SetActive(true); 
            }
        }
        
    }

    void update()
    {
        
    }
}
