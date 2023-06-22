using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLoad : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<SaveLoadScript>().LoadGame();
    }
}
