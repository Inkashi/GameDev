using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform attackPoint;
    public GameObject LaserBeam;

    public GameObject LightLaser;
    EntityLook BossLook;
    private Animator anim;
    public bool reverse = false;
    private float time =0;
    private Transform player;
    private float angle; 
    void Start()
    {
        anim = GetComponent<Animator>();
        BossLook = GetComponent<EntityLook>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
         BossLook.LookAtPlayer();
         time -=Time.deltaTime;
        if(time<= 0) {
            anim.SetTrigger("Laser");
            time = 15;
            reverse = !reverse;
        }    
       
    }
     void FixedUpdate()
    {
        Vector2 lookDir = player.position - attackPoint.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        attackPoint.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }
    void LightLasAttck() 
    {
        Instantiate(LightLaser, attackPoint.position, attackPoint.rotation);
    }
    void LasAttck() {
        attackPoint.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        Instantiate(LaserBeam, attackPoint.position, attackPoint.rotation);
    }
}
