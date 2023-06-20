using UnityEngine;

public class Traps : MonoBehaviour
{
    public int attackDamage = 10;
    private Animator anim;
    Collider2D Hero;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("HeroNear");
        }
    }

    public void HitHero()
    {
        Collider2D[] Enemyes = Physics2D.OverlapCircleAll(transform.position, 1f, LayerMask.GetMask("Player"));
        Enemyes[0].GetComponent<HeroMovement>().TakeDamage(attackDamage);
    }
}
