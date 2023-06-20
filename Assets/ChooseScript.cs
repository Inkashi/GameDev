using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseScript : MonoBehaviour
{
    public Animator darkscreen;
    public int LevelToIn;
    public void clickOnYES()
    {
        LevelToIn = 3;
        darkscreen.SetTrigger("NextToLevel");
    }

    public void clickOnNO()
    {
        LevelToIn = 0;
        darkscreen.SetTrigger("NextToLevel");
    }
}
