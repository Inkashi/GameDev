using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D EnemyRb;
    
    public Transform Hero;
    private float AgroDistance = 10f;
    private float speed = 1f;
    void Start()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, Hero.position);

        if (distToPlayer < AgroDistance)
            StartHunt();
        else
        {
            StopHunt();
        }
    }
    void StartHunt() {
        if(Hero.position.x < transform.position.x) {
            EnemyRb.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(-1,1);
        }
        else {
            EnemyRb.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(1,1);
        }
    }
     void StopHunt() 
     {
        EnemyRb.velocity = new Vector2(0,0);
    }
}
