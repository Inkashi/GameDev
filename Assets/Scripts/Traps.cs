using UnityEngine;

public class Traps : MonoBehaviour
{
    private int attackDamage = 10;
    private Animator anim;
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
            Debug.Log("¿È");
            other.GetComponent<HeroMovement>().TakeDamage(attackDamage);
        }
    }
}
