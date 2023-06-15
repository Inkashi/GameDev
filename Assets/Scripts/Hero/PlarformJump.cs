using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlarformJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private int player;
    private int platforms;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            jumpPlatform();
            rb.velocity = new Vector2(0f, -1f);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            jumpPlatform();
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = LayerMask.NameToLayer("Player");
        platforms = LayerMask.NameToLayer("platform");
    }
    public void jumpPlatform()
    {
        StartCoroutine(IgnoreCollider());
    }

    private IEnumerator IgnoreCollider()
    {
        Physics2D.IgnoreLayerCollision(player, platforms, true);
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreLayerCollision(player, platforms, false);
    }
}
