using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hui : MonoBehaviour
{
    private void Start()
    {
        Camera.main.GetComponent<SaveLoadScript>().LoadGame();
    }
}
