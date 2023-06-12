using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necr : MonoBehaviour
{
    public int health; 


    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
           // anim.SetTrigger("Hurt");
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
        //anim.SetBool("IsDead", true);
    }
}
