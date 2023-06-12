using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseButtons : MonoBehaviour
{
    public void Qquit()
    {
        SceneManager.LoadScene(0);
    }
    public void SettingsMenu()
    {
        SceneManager.LoadScene(2);
    }
}
