
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossControl : MonoBehaviour
{
    public int health;
    private Transform player;
    public Slider slider;
    public bool isFlipped = true;
    private bool Less= false;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        SetHealth(health);
        if (health <3100 && !Less) {
            StartCoroutine(Tele());
            GetComponent<Boss>().Firstphase = false;
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

        Less = true;
           for(int i = 0;i < 5 ; i++) { 
                Color c = srs.color;
                c.a -= 0.2f;
                srs.color = c;
                 yield return new WaitForSeconds(0.1f);
         }
         yield return new WaitForSeconds(0.4f);
               Teleport();
                for(int i = 0;i < 5 ; i++) { 
                Color c = srs.color;
                c.a += 0.2f;
                srs.color = c;
                 yield return new WaitForSeconds(0.1f);
         }
                
        
        
              
    }
    public void Teleport()
    {
        transform.position = new Vector2(0.15f, -0.42f);
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
}