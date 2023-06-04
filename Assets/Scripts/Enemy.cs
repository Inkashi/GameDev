using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public GameObject deathEffect;
    public GameObject Point;

    private void Update()
    {
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Instantiate(Point, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

}
