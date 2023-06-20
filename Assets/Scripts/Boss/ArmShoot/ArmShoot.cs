using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmShoot : MonoBehaviour
{
    // Start is called before the first frame update
      public float speed = 1f;
      public int ArmDamage = 15;
     void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
     private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.name == "MainHero") 
        {
            collision.GetComponent<HeroMovement>().TakeDamage(ArmDamage);
        }

    }
     private void OnTriggerStay2D(Collider2D collision) {
        if(collision.name == "MainHero") 
        {
            collision.GetComponent<HeroMovement>().TakeDamage(ArmDamage);
        }

    }
}
