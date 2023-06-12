using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Range = 0.2f;

    public int attackDamage = 10;
    public float speed = 10f;
    public LayerMask player;

    Transform pl;
    
    
    void Start() {
         pl = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update() {

        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

  
    void OnCollisionEnter2D(Collision2D collision) {        
        Collider2D Player = Physics2D.OverlapCircle(transform.position, Range, player);
        if (Player != null) 
        {
        Player.GetComponent<HeroMovement>().TakeDamage(attackDamage);
        }
        //Destroy(effect,3f);
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
