using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscene : MonoBehaviour
{
    public GameObject choose;
    public void nextlevel()
    {
        SceneManager.LoadScene(choose.GetComponent<ChooseScript>().LevelToIn);
    }
}
