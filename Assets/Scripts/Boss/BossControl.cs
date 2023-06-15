
using UnityEngine;
using UnityEngine.UI;

public class BossControl : MonoBehaviour
{
    public int health;
    private Transform player;
    public Slider slider;
    public bool isFlipped = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        SetHealth(health);
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

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Die");
        //Смерть добавить
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
}