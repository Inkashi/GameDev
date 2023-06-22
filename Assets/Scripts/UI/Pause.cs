using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausepanel;
    public GameObject settings;
    private bool pauseOpen = false;

    private void Awake()
    {
        pausepanel.SetActive(pauseOpen);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            switch (pauseOpen)
            {
                case false:
                    SetPause();
                    break;
                case true:
                    if (settings.activeSelf == true)
                    {
                        PauseOff(settings.transform.GetChild(0));
                    }
                    else
                    {
                        PauseOff(pausepanel.transform.GetChild(0));
                    }
                    break;
            }
        }
    }

    public void SetPause()
    {
        Time.timeScale = 0f;
        pauseOpen = !pauseOpen;
        pausepanel.SetActive(pauseOpen);
    }

    public void PauseOff(Transform button)
    {
        pauseOpen = !pauseOpen;
        Transform temp_parent = button.parent;
        temp_parent.gameObject.SetActive(pauseOpen);
        Time.timeScale = 1f;
    }
    public void tryAgain()
    {
        SceneManager.LoadScene(4);
        Time.timeScale = 1f;
    }

    public void Qquit()
    {
        SceneManager.LoadScene(0);
        pausepanel.SetActive(false);
    }
    public void SettingsMenu()
    {
        settings.SetActive(true);
        pausepanel.SetActive(false);
    }
}
