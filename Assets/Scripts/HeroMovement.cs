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
    public float DashImpulse = 10f;

    [Header("InitObjects")]
    public TextMeshProUGUI PointDisplay;
    public GameObject Alarm;
    public GameObject statsMenu;
    public Transform attackPos;
    public Slider slider;
    public LayerMask Enemy;
    public KeyCode dashKey = KeyCode.LeftShift;

    //Приватные элементы
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Transform tranform;
    private bool Attacking = false;
    private bool Recharged = true;
    private bool onGround = false;
    public bool Archontnear = false;
    private bool Dashcharged = true;


    private void Start()
    {
        SetMaxHealth(health);
    }
    private void FixedUpdate()
    {
        CheckSky();
    }
    private void Update()
    {
        SetHealth(health);
        if (onGround) State = States.idle;
        if (Input.GetButton("Horizontal"))
            Run();
        if (Input.GetButtonDown("Jump") && onGround)
            Jump();
        if (tranform.position.y < -10)
            Respawn();
        if (Input.GetButtonDown("Fire1"))
            Attack();
        if (Input.GetKeyDown(KeyCode.E) && Archontnear == true)
        {
            statsMenu.SetActive(true);
        }
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        tranform = GetComponent<Transform>();
        Recharged = true;
        attackPos = GetComponent<Transform>();
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

    private void OnAttack()
    {
        Vector2 attackPosition = attackPos.position;
        if (!sprite.flipX)
        {
            attackPosition.x -= attackRange;
        }
        else
        {
            attackPosition.x += attackRange;
        }
        Collider2D[] Enemyes = Physics2D.OverlapCircleAll(attackPos.position, attackRange, Enemy);
        foreach (Collider2D enemy in Enemyes)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("Eneme has " + enemy.GetComponent<Enemy>().health);
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
    private void Respawn()
    {
        tranform.position = new Vector3(0, 0, 0);
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
    }

    private void Dash()
    {
        if (Dashcharged)
        {
            anim.StopPlayback();
            anim.SetTrigger("Dash");
            rb.velocity = new Vector2(0, 0);
            if (sprite.flipX)
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

    private IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(1f);
        Dashcharged = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "point":
                PlayerPrefs.GetInt("points");
                pointsCount += 1;
                PlayerPrefs.SetInt("points", pointsCount);
                PointDisplay.text = pointsCount.ToString();
                Destroy(other.gameObject);
                break;
            case "healthPotion":
                health += 20;
                Destroy(other.gameObject);
                break;
            case "Archont":
                Alarm.SetActive(true);
                Archontnear = true;
                break;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Archont"))
        {
            Alarm.SetActive(false);
            Archontnear = false;
        }
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}

public enum States
{
    idle, run, jump,
}
