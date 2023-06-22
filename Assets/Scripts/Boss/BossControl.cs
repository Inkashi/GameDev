
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossControl : MonoBehaviour
{
    [SerializeField] private AudioSource BossDamageAudio;
    public bool immortal = false;
    public int health;
    private Transform player;
    public Slider slider;
    public bool IsDead = false;
    public bool isFlipped = true;
    public GameObject exit;
    private bool Less = false;

    private bool onGround = false;

    private Rigidbody2D rb;
    public Transform GroundCheck;
    private Animator anim;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetHealth(health);
        if (health < 1750 && !Less)
        {
            Less = true;
            anim.SetTrigger("ProtectUp");
            GetComponent<Boss>().Firstphase = false;
            anim.ResetTrigger("Wind");
            anim.ResetTrigger("Laser");
            anim.ResetTrigger("Arm");
        }
        if (IsDead)
        {
            CheckGround();
        }
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }
    private IEnumerator Tele()
    {
        SpriteRenderer srs = GetComponent<SpriteRenderer>();


        for (int i = 0; i < 5; i++)
        {
            Color c = srs.color;
            c.a -= 0.2f;
            srs.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.4f);
        Teleport();
        for (int i = 0; i < 5; i++)
        {
            Color c = srs.color;
            c.a += 0.2f;
            srs.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        anim.SetTrigger("Charge");
    }

    public void Teleport()
    {
        transform.position = new Vector2(0.15f, -0.42f);
    }

    public void Teleporting()
    {
        StartCoroutine(Tele());
    }
    public void TakeDamage(int damage)
    {
        if (immortal)
        {
            return;
        }
        BossDamageAudio.Play();
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !IsDead && !immortal)
        {
            collision.GetComponent<HeroMovement>().TakeDamage(10);
        }
    }
    void Die()
    {
        IsDead = true;
        exit.SetActive(true);
        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.SetBool("IsDead", true);
        GetComponent<EntityLook>().enabled = false;
    }
    void Destroyed()
    {
        Destroy(this.gameObject);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth()
    {
        slider.maxValue = health;
        slider.value = health;
        slider.gameObject.SetActive(true);
    }
    private void CheckGround()
    {
        RaycastHit2D hitground = Physics2D.Raycast(GroundCheck.position, new Vector2(0, -0.1f), 0.01f, LayerMask.GetMask("Default")); ;
        onGround = hitground;
        if (onGround)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
}