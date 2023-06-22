using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform attackPoint;
    public Transform WindAttackPoint;
    public Transform ShootAttackPoint;
    public GameObject LaserBeam;
    public GameObject LightLaser;
    public GameObject Arm;
    public GameObject WindFlow;
    BossControl BossLook;
    private Animator anim;
    public bool Firstphase = true;
    public bool reverse = false;
    private float time = 0;
    private Transform player;
    private float angle;
    [SerializeField] private AudioSource ShootAttckAudio;
    [SerializeField] private AudioSource WindAttckAudio;
    [SerializeField] private AudioSource LasAttckAudio;
    [SerializeField] private AudioSource LightLasAttckAudio;

    int rand;
    private bool Recharge = true;
    void Start()
    {
        anim = GetComponent<Animator>();
        BossLook = GetComponent<BossControl>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (BossLook.IsDead != true)
        {
            BossLook.LookAtPlayer();
        }
        if (Firstphase)
        {
            if (Recharge && player.position.y > transform.position.y + 0.4f)
            {
                Recharge = false;
                anim.SetTrigger("Arm");
            }
            else if (Recharge && player.position.y <= transform.position.y + 0.4f)
            {
                Recharge = false;
                anim.SetTrigger("Wind");
            }
        }
        else
        {
            if (Recharge)
            {
                Recharge = false;
                if (Random.Range(1, 3) == 1)
                {
                    anim.SetTrigger("LightLaser");
                }
                else
                {
                    anim.SetTrigger("Laser");
                }
            }

        }

    }
    void FixedUpdate()
    {
        Vector2 lookDir = player.position - attackPoint.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        attackPoint.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        Vector2 ArmLookDir = player.position - ShootAttackPoint.position;
        float Armangle = Mathf.Atan2(ArmLookDir.y, ArmLookDir.x) * Mathf.Rad2Deg;
        ShootAttackPoint.rotation = Quaternion.Euler(0f, 0f, Armangle - 90f);

    }

    void LightLasAttck()
    {
        GetComponentInChildren<LightLaserControl>().attck();
        //Instantiate(LightLaser, attackPoint.position, attackPoint.rotation);
        LightLasAttckAudio.Play();

    }
    void LasAttck()
    {
        // attackPoint.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        reverse = !reverse;
        Instantiate(LaserBeam, attackPoint.position, attackPoint.rotation);
        anim.ResetTrigger("Wind");
        anim.ResetTrigger("Laser");
        LasAttckAudio.Play();
        anim.ResetTrigger("Arm");

    }
    void WindAttck()
    {
        Instantiate(WindFlow, WindAttackPoint.position, Quaternion.identity);
        anim.ResetTrigger("Wind");
        WindAttckAudio.Play();
        anim.ResetTrigger("Laser");
        anim.ResetTrigger("Arm");
    }
    void ShootAttck()
    {
        Instantiate(Arm, ShootAttackPoint.position, ShootAttackPoint.rotation);
        ShootAttckAudio.Play();
        anim.ResetTrigger("Wind");
        anim.ResetTrigger("Laser");
        anim.ResetTrigger("Arm");
    }

    public void cooldown()
    {
        StartCoroutine(AttackCoolDown());
    }
    public IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(2.5f);
        Recharge = true;
    }


}
