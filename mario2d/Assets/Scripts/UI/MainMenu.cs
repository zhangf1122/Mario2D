using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //(取消暂停)载入场景1
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("World0-1");
    }

    public void QuitGame()
    {
        //退出程序(.exe格式下有效)
        Application.Quit();
    }
}
