using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsColl : MonoBehaviour
{
    int pointscoll;
    int groundcoll;
    private bool onGround = false;
    private Rigidbody2D rb;
    private void Update()
    {
        CheckGround();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void CheckGround()
    {
        RaycastHit2D hitground = Physics2D.Raycast(transform.position, new Vector2(0, -0.1f), 0.1f, LayerMask.GetMask("Default")); ;
        onGround = hitground;
        if (onGround)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}
