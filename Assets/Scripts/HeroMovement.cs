using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class HeroMovement : MonoBehaviour
{
    [Header("InitHeroStats")]
    public float speed = 3f;
    public float jumpForce = 2f;
    public int health = 100;
    public int damage = 10;
    public float attackRange;
    public int pointsCount = 0;
    public float DashImpulse = 10f;

    [Header("InitObjects")]
    public TextMeshProUGUI PointDisplay;
    public Transform attackPos;
    public Slider slider;
    public LayerMask Enemy;
    public KeyCode dashKey = KeyCode.LeftShift;
    public bool DashAccses = false;
    public bool DoubleJumpAccses = false;
    public bool ThrowAccses = false;

    //Приватные элементы
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Collider2D heroColl;
    public static Transform tranform;
    private bool Attacking = false;
    private bool Recharged = true;
    private bool onGround = false;
    private bool Dashcharged = true;
    private int DoubleJumpCharged = 0;
    public bool isFlipped = true;


    private void Start()
    {
        transform.position = new Vector2(52.332f, -2.572f);
        SetMaxHealth(health);
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Update()
    {
        SetHealth(health);
        if (onGround)
        {
            State = States.idle;
            if (DoubleJumpAccses)
            {
                DoubleJumpCharged = 1;
            }
            else
            {
                DoubleJumpCharged = 0;
            }
        }
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetKeyDown(dashKey))
        {
            if (DashAccses)
                Dash();
        }
        if (Input.GetButtonDown("Jump") && (onGround || DoubleJumpCharged > 0))
            Jump();
        if (Input.GetButtonDown("Fire1"))
            Attack();
    }

    private void Awake()
    {
        heroColl = GetComponent<Collider2D>();
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
        if (Input.GetAxis("Horizontal") > 0 && !isFlipped)
        {
            Flip();
        }
        else if (Input.GetAxis("Horizontal") < 0 && isFlipped)
        {
            Flip();
        }
    }
    private void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        tranform.localScale = theScale;
    }

    private void Jump()
    {
        DoubleJumpCharged--;
        rb.velocity = new Vector2(0.0f, jumpForce);

    }

    private void CheckGround()
    {
        RaycastHit2D hitground = Physics2D.Raycast(transform.position, new Vector2(0, -1), 0.3f, LayerMask.GetMask("Default"));
        onGround = hitground;
        if (!onGround)
        {
            State = States.jump;
        }
    }

    private void OnAttack()
    {
        Vector2 attackPosition = attackPos.position;

        Collider2D[] Enemyes = Physics2D.OverlapCircleAll(attackPos.position, attackRange, Enemy);
        foreach (Collider2D enemy in Enemyes)
        {
            if (enemy.tag == "barrel")
            {
                enemy.GetComponent<BarrelScript>().TakeDamage(damage);
            }
            else
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
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

    public void TakeDamage(int damage)
    {

        health -= damage;
        Debug.Log(health);

        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
        GetComponent<DieMenu>().showDiemenu();
    }

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    private void Dash()
    {
        if (Dashcharged)
        {
            anim.StopPlayback();
            anim.SetTrigger("Dash");
            rb.velocity = new Vector2(0, 0);
            if (!isFlipped)
            {
                rb.AddForce(Vector2.left * DashImpulse, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.right * DashImpulse, ForceMode2D.Impulse);
            }

            Dashcharged = false;
            StartCoroutine(DashCoolDown());
        }
        Collider2D[] Enemyes = Physics2D.OverlapCircleAll(tranform.position, 1, Enemy);
        foreach (Collider2D enemy in Enemyes)
        {
            StartCoroutine(IgnoreCollider(enemy));
        }
    }

    private IEnumerator IgnoreCollider(Collider2D other)
    {
        Physics2D.IgnoreCollision(heroColl, other, true);
        yield return new WaitForSeconds(0.1f);
        Physics2D.IgnoreCollision(heroColl, other, false);
    }

    private IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(1f);
        Dashcharged = true;
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "point":
                PlayerPrefs.GetInt("points");
                pointsCount += Random.Range(1, 10);
                PlayerPrefs.SetInt("points", pointsCount);
                PointDisplay.text = pointsCount.ToString();
                Destroy(other.gameObject);
                break;
            case "healthPotion":
                health += Random.Range(10, 40);
                Destroy(other.gameObject);
                break;
        }
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetPoint(int point)
    {
        pointsCount -= point;
        PlayerPrefs.SetInt("points", pointsCount);
        PointDisplay.text = pointsCount.ToString();
    }
}

public enum States
{
    idle, run, jump,
}
