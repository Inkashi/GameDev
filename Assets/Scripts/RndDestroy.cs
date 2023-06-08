using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RndDestroy : MonoBehaviour
{
    public float DestroyChance = 0.5f;
    void Start()
    {
        if (Random.value < DestroyChance)
        {
            Destroy(gameObject);
        }
    }
}
