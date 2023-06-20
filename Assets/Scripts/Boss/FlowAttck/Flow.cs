using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    // Start is called before the first frame update
   public float speed = 1f;
    private float playerX;
    private float BossX;
    private int FlowDamage = 10;
    private SpriteRenderer sprite;
    void Start () {
        playerX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        sprite = GetComponent<SpriteRenderer>();
        BossX = GameObject.FindGameObjectWithTag("Boss").transform.position.x;
    }
     void Update()
    {
        if (playerX > BossX) {
            sprite.flipX = false;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        }
        else {
            sprite.flipX = true;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.name == "MainHero") 
        {
            collision.GetComponent<HeroMovement>().TakeDamage(FlowDamage);
        }

    }
     private void OnTriggerStay2D(Collider2D collision) {
        if(collision.name == "MainHero") 
        {
            collision.GetComponent<HeroMovement>().TakeDamage(FlowDamage);
        }

    }
    
}
