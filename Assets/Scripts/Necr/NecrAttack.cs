using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecrAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    private Transform Necr;
    public GameObject projectile;

    private float projectileForce = 5f;
    public Transform AttackPoint;
    Animator anim;
    //public Rigidbody2D rb;
    EntityLook Look;
    void Start()
    {
        Necr = GetComponent<Transform>().transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        Look = GetComponent<EntityLook>();
    }

    // Update is called once per frame
    void Update()
    {
        Look.LookAtPlayer();
        float Dist = Vector2.Distance(player.position, Necr.position);
        anim.SetFloat("AgrRange", Dist);

    }
    void FixedUpdate()
    {
        Vector2 lookDir = player.position - AttackPoint.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        AttackPoint.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }
    void Attack()
    {
        GameObject Project = Instantiate(projectile, AttackPoint.position, AttackPoint.rotation);
        // Transform rproject = Project.GetComponent<Transform>();
        // rproject.Translate(Vector2.up * projectileForce *Time.deltaTime);
        // Rigidbody2D rproject = Project.GetComponent<Rigidbody2D>();
        // rproject.AddForce(AttackPoint.up * projectileForce, ForceMode2D.Impulse);

    }

}
