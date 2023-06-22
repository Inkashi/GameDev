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
    //Spells Accses
    public bool DashAccses = false;
    public bool DoubleJumpAccses = false;
    public bool ThrowAccses = false;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Collider2D heroColl;
    private float DashImpulse = 2f;
    private float timer = 0;
    private int DoubleJumpCharged = 0;
    private bool Attacking = false;
    private bool Recharged = true;
    private bool onGround = false;
    private bool Dashcharged = true;
    public bool isFlipped = true;
    private bool DamageCD = false;
    private bool OnPause = false;
    private bool IsDead = false;
    private KeyCode dashKey = KeyCode.LeftShift;
    //Audio
    [SerializeField] private AudioSource JumpAudio;
    [SerializeField] private AudioSource DashAudio;
    [SerializeField] private AudioSource PointAudio;
    [SerializeField] private AudioSource DamageAudio;
    [SerializeField] private AudioSource AttackAudio;
    [SerializeField] private AudioSource DieAudio;


    private void Start()
    {
        SetPoint(0);
        SetMaxHealth(100);
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Update()
    {
        if (!IsDead)
        {
            timer += Time.deltaTime;
            SetHealth(health);
            anim.SetBool("OnGround", onGround);
            anim.SetFloat("Timer", timer);
            if (Time.timeScale == 0)
            {
                OnPause = true;
            }
            else
            {
                OnPause = false;
            }
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
            if (Input.GetButton("Horizontal") && !OnPause)
                Run();
            if (Input.GetKeyDown(dashKey) && !OnPause)
            {
                if (DashAccses)
                    Dash();
            }
            if (Input.GetButtonDown("Jump") && (onGround || DoubleJumpCharged > 0) && !OnPause)
                Jump();
            if (Input.GetButtonDown("Fire1") && !OnPause)
                Attack();
        }
    }


    private void Awake()
    {
        heroColl = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
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
    public void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
            JumpAudio.Play();
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
            else if (enemy.tag == "Eye")
            {
                enemy.GetComponent<EyeControll>().TakeDamage(damage);
            }
            else
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    private void Attack()
    {
        if (timer < 0.6 && !OnPause)
        {
            AttackAudio.Play();
            anim.SetTrigger("ScndAttack");
        }
        if (Recharged && !OnPause)
        {
            timer = 0;
            anim.SetTrigger("Attack");
            Attacking = true;
            Recharged = false;
            if (onGround)
                StartCoroutine(AttackCoolDown());
            else
                StartCoroutine(JumpAttackCoolDown());
        }
    }

    public void TakeDamage(int damage)
    {
        if (!DamageCD)
        {
            health -= damage;
            DamageAudio.Play();
            DamageCD = true;
            StartCoroutine(DamageCoolDown());
            if (health <= 0)
            {
                Die();
            }
        }
    }
    void Die()
    {
        IsDead = true;
        DieAudio.Play();
        anim.SetTrigger("Die");
        DamageCD = true;
        GetComponent<DieMenu>().showDiemenu();
    }

    public void post_die()
    {
        Time.timeScale = 0;
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
            DamageCD = true;
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
            DashAudio.Play();
            Dashcharged = false;
            StartCoroutine(DashCoolDown());
        }
        Collider2D[] Enemyes = Physics2D.OverlapCircleAll(transform.position, 1, Enemy);
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
        SetHealth(health);
    }
    private IEnumerator JumpAttackCoolDown()
    {
        yield return new WaitForSeconds(0.4f);
        Recharged = true;
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
        DamageCD = false;
    }

    private IEnumerator AttackCoolDown()
    {
        AttackAudio.Play();
        yield return new WaitForSeconds(1f);
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
                pointsCount += Random.Range(1, 20);
                PlayerPrefs.SetInt("points", pointsCount);
                PointAudio.Play();
                PointDisplay.text = pointsCount.ToString();
                Destroy(other.gameObject);
                break;
            case "healthPotion":
                if (health >= 80)
                {
                    health = 100;
                    Destroy(other.gameObject);
                }
                else if (health == 100)
                {
                }
                else
                {
                    health += 20;
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
