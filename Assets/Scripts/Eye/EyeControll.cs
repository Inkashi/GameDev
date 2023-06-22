using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeControll : MonoBehaviour
{
    public int health;
    public GameObject dieEffect;
    public GameObject[] bonus;
    int PlayerMask;
    int EyeMASK;
    bool AttackCD = false;
    void Start()
    {
        PlayerMask = LayerMask.NameToLayer("Player");
        EyeMASK = LayerMask.NameToLayer("Eye");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<HeroMovement>().TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        gameObject.GetComponent<HpEnemy>().UpdateHealEnemy();
        if (health <= 0)
        {
            GameObject Effect = Instantiate(dieEffect, transform.parent.position, Quaternion.identity);
            Instantiate(bonus[Random.Range(0, 1)], transform.parent.position, Quaternion.identity);
            Destroy(Effect, 2f);
            Destroy(transform.parent.gameObject);
        }
    }
}
