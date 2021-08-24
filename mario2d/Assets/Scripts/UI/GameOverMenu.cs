using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject GameOverUI;
    private Rigidbody2D playerRigid;
    void Start()
    {
        playerRigid = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (playerRigid == null)
        {
            Pause();
        }
    }

    /// <summary>
    /// 游戏暂停
    /// </summary>
    public void Pause()
    {
        GameOverUI.SetActive(true);
        ScoreText.ScoreCount = 0;
        GoldText.GoldCount = 0;
        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// 回到主菜单
    /// </summary>
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
}
