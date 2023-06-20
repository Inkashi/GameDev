using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlarformJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            jumpPlatform();
            rb.velocity = new Vector2(0f, -1f);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void jumpPlatform()
    {
        Collider2D[] Platforms = Physics2D.OverlapCircleAll(gameObject.transform.position, 1f, LayerMask.GetMask("platform"));
        foreach (Collider2D p in Platforms)
        {
            StartCoroutine(IgnoreCollider(p.transform));
        }
    }

    private IEnumerator IgnoreCollider(Transform platform)
    {
        platform.GetComponent<PlatformEffector2D>().rotationalOffset = 180;
        yield return new WaitForSeconds(0.3f);
        platform.GetComponent<PlatformEffector2D>().rotationalOffset = 0;
    }
}
