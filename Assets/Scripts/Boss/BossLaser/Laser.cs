using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
   
    public int LaserDamage = 10;
   
   
   
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.name == "MainHero") 
        {
            collision.GetComponent<HeroMovement>().TakeDamage(LaserDamage);
        }

    }
}
