using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeroMovement : MonoBehaviour
{
    [Header("InitHeroStats")]
    public float speed = 3f;
    public float jumpForce = 2f;
    public int health = 100;
    public int damage = 10;
    public float attackRange;
    public int pointsCount = 0;

    [Header("InitObjects")]
    public TextMeshProUGUI PointDisplay;
    public Transform attackPos;
    public Slider slider;
    public GameObject JumpEffect;
    public LayerMask Enemy;
    public bool DashAccses = false;
    public bool DoubleJumpAccses = false;
    public bool ThrowAccses = false;

    //Приватные элементы
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Collider2D heroColl;
    private Transform tranform;
    private float DashImpulse = 2f;
    private int DoubleJumpCharged = 0;
    private bool Attacking = false;
    private bool Recharged = true;
    private bool onGround = false;
    private bool Dashcharged = true;
    private bool isFlipped = true;
    private bool DamageCD = false;
    private KeyCode dashKey = KeyCode.LeftShift;


    private void Start()
    {
        SetPoint(0);
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
        if (DoubleJumpCharged > 0)
        {
            anim.SetTrigger("doubleJump");
        }
        if (!onGround && DoubleJumpAccses)
        {
            GameObject effect = Instantiate(JumpEffect, transform.position, Quaternion.identity);
            Destroy(effect, 3f);
        }
        DoubleJumpCharged--;
        rb.velocity = new Vector2(0.0f, jumpForce);
    }

    private void CheckGround()
    {
        RaycastHit2D hitground = Physics2D.Raycast(transform.position, new Vector2(0, -1), 0.3f, LayerMask.GetMask("Default") + LayerMask.GetMask("platform"));
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
            else if (enemy.tag == "Boss")
            {
                enemy.GetComponent<BossControl>().TakeDamage(damage);
            }
            else
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    private void Attack()
    {
        if (Recharged)
        {
            anim.SetTrigger("Attack");
            Attacking = true;
            Recharged = false;
            StartCoroutine(AttackCoolDown());
        }
    }

    public void TakeDamage(int damage)
    {
        if (!DamageCD)
        {
            health -= damage;

            if (health <= 0)
            {
                Die();
            }
            DamageCD = true;
            StartCoroutine(DamageCoolDown());
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

    public void LoadHeroData(Save.HeroSaveData save)
    {
        health = save.Health;
        DoubleJumpAccses = save.DoubleJumpAccses;
        DashAccses = save.DashAccses;
        damage = save.Damage;
        pointsCount = save.pointsCount;
        ThrowAccses = save.ThrowAccses;
        SetPoint(0);
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

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        Recharged = true;
    }

    private IEnumerator DamageCoolDown()
    {
        for (int i = 0; i < 5; i++)
        {
            Color c = sprite.color;
            c.a = 0;
            sprite.color = c;
            yield return new WaitForSeconds(.1f);
            c = sprite.color;
            c.a = 1;
            sprite.color = c;
            yield return new WaitForSeconds(.1f);
        }
        DamageCD = false;
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
                if (health < 100)
                {
                    health += 10;
                    Destroy(other.gameObject);
                }
                break;
        }
    }
}

public enum States
{
    idle, run, jump,
}
