using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 100;
    [SerializeField] private float jumpForce = 2f;

    private bool onGround = false;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Transform tranform;
    public Transform attackPos;

    private bool Attacking = false;
    private bool Recharged = true;

    public float attackRange;
    public LayerMask Enemy;
    public int damage = 1;

    private void FixedUpdate()
    {
        CheckSky();
    }

    private void Update()
    {
        if (onGround) State = States.idle;
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButtonDown("Jump") && onGround)
            Jump();
        if (tranform.position.y < -10)
            Respawn();
        if (Input.GetButtonDown("Fire1"))
            Attack();
    }
    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        tranform = GetComponent<Transform>();
        Recharged = true;
    }

    private void Run()
    {
        if (onGround) State = States.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckSky()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        onGround = collider.Length > 1;
        if (!onGround) State = States.jump;
    }
    private void Respawn()
    {
        tranform.position = new Vector3(0, 0, 0);
    }
    private void Attack()
    {
        if (onGround && Recharged)
        {
            anim.SetTrigger("Attack");
            Attacking = true;
            Recharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private void OnAttack()
    {
        Collider2D[] Enemyes = Physics2D.OverlapCircleAll(attackPos.position, attackRange, Enemy);
        foreach (Collider2D enemy in Enemyes)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("Eneme has " + enemy.GetComponent<Enemy>().health);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        Attacking = false;
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        Recharged = true;
    }
}

public enum States
{
    idle, run, jump,
}
