using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platfromFly : MonoBehaviour
{
    public float Speed;
    public Transform start, finish;
    bool moveToLeft = true;
    // Update is called once per frame

    private void Start()
    {
        transform.position = new Vector2(start.position.x, transform.position.y);
    }
    void Update()
    {
        if (transform.position.x < start.position.x)
        {
            moveToLeft = false;
        }
        else if (transform.position.x > finish.position.x)
        {
            moveToLeft = true;
        }
        if (moveToLeft)
        {
            transform.position = new Vector2(transform.position.x - Speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x + Speed * Time.deltaTime, transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
