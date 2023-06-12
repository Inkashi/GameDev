using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausepanel;
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
                    PauseOff();
                    break;
            }
        }
    }

    public void SetPause()
    {
        pauseOpen = !pauseOpen;
        pausepanel.SetActive(pauseOpen);
        Time.timeScale = 0f;
    }

    public void PauseOff()
    {
        pauseOpen = !pauseOpen;
        pausepanel.SetActive(pauseOpen);
        Time.timeScale = 1f;
    }
}
