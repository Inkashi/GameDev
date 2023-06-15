using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heroknife : MonoBehaviour
{
    public float speed = 4f;
    public LayerMask Enemy;
    public LayerMask Player;
    private int knife;
    private int platform;
    public int attackDamage = 10;
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    private void Start()
    {
        knife = LayerMask.NameToLayer("Knife");
        platform = LayerMask.NameToLayer("platform");
        Physics2D.IgnoreLayerCollision(knife, platform, true);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D Enemyes = Physics2D.OverlapCircle(transform.position, 0.3f, Enemy);
        Collider2D Playe = Physics2D.OverlapCircle(transform.position, 0.1f, Player);

        if (Enemyes != null)
        {
            if (Enemyes.tag == "Boss")
            {
                Enemyes.GetComponent<BossControl>().TakeDamage(attackDamage);
            }
            else
            {
                Enemyes.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
            Destroy(this.gameObject);
        }
        if (Playe == null)
        {
            Destroy(gameObject);
        }
    }
}
