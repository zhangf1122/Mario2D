using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public GameObject WinUI;
    /// <summary>
    /// 重新游戏
    /// </summary>
    public void ReStart()
    {
        WinUI.SetActive(false);
        SceneManager.LoadScene(1);
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
