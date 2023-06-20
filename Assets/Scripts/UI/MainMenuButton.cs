using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public static int PrevScene;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Debug.Log("Game is close");
        Application.Quit();
    }

    public void settings()
    {
        PrevScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(PrevScene);
    }
}
