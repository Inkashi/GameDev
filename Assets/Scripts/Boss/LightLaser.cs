using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLaser : MonoBehaviour
{
      public float speed = 1f;
      public int LightLaserDamage = 15;
     void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "MainHero") {
            other.GetComponent<HeroMovement>().TakeDamage(LightLaserDamage);
        }
    }
}
