using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int health;

    public GameObject deathEffect;
    public GameObject Point;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            anim.SetTrigger("Hurt");
        }
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = false;
        anim.SetBool("IsDead", true);
    }

    public void Destroyed()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Instantiate(Point, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
