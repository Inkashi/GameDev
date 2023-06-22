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
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision != null)
        {
            if (collision.tag == "Boss")
            {
                collision.GetComponent<BossControl>().TakeDamage(attackDamage);
                Destroy(this.gameObject);
            }
            else if (collision.tag == "Eye")
            {
                collision.GetComponent<EyeControll>().TakeDamage(attackDamage);
                Destroy(this.gameObject);
            }
            else if (collision.gameObject.layer == 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                collision.GetComponent<Enemy>().TakeDamage(attackDamage);
                Destroy(this.gameObject);
            }
        }
    }
}
