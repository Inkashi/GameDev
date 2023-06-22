using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundInstanceControllerComponent : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] private string createdTag;

    private void Awake()
    {
        GameObject obj = GameObject.FindWithTag(this.createdTag);
        Debug.Log(obj);
        if (obj != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.tag = this.createdTag;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
