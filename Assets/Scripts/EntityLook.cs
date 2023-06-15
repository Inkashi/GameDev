using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLook : MonoBehaviour
{

    private Transform player;

    public bool isFlipped = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            transform.Find("hpbar").gameObject.transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            transform.Find("hpbar").gameObject.transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

}