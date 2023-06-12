using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heroknife : MonoBehaviour
{
    public float speed = 4f;
    public LayerMask Enemy;
    public LayerMask Player;
    public int attackDamage = 10;
    void Update()
    {
        transform.Translate(Vector2.up * speed *Time.deltaTime);
    }

       void OnTriggerEnter2D(Collider2D collision) {        
         Collider2D Enemyes = Physics2D.OverlapCircle(transform.position, 0.3f, Enemy);
         Collider2D Playe = Physics2D.OverlapCircle(transform.position, 0.3f, Player);

            if (Enemyes != null)  {
            Enemyes.GetComponent<Enemy>().TakeDamage(attackDamage);
            Destroy(this.gameObject);
            }
            if(Playe ==null) {
                Destroy(gameObject);
            }
        }
        
        
       


}
