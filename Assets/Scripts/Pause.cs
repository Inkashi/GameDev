using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausepanel;

    private void Awake()
    {
        pausepanel.SetActive(false);
    }

    public void SetPause()
    {
        pausepanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        pausepanel.SetActive(false);
        Time.timeScale = 1;
    }
}
