using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroThrow : MonoBehaviour
{
    Vector3 mousePos;
    private Animator anim;
    private bool ChargedThrow = true;
    int knifes;
    int platform;
    public Transform ThrowPos;
    public GameObject knife;
    public Camera cam;
    Vector2 lookDir;
    private bool flip;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        knifes = LayerMask.NameToLayer("Knife");
        platform = LayerMask.NameToLayer("platform");
        Physics2D.IgnoreLayerCollision(knifes, platform, true);
    }
    void Update()
    {
        flip = GetComponent<HeroMovement>().isFlipped;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - ThrowPos.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        ThrowPos.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        if (Input.GetMouseButtonDown(1) && gameObject.GetComponent<HeroMovement>().ThrowAccses)
        {
            Throw();
        }
    }
    public void Throw()
    {
        if (ChargedThrow)
        {
            ChargedThrow = false;
            anim.SetTrigger("Throw");
            if (flip == false && mousePos.x > transform.position.x)
            {
                GetComponent<HeroMovement>().Flip();
            }
            else if (flip == true && mousePos.x < transform.position.x)
            {
                GetComponent<HeroMovement>().Flip();
            }
            LastCordinate();

            GameObject tempknife = Instantiate(knife, ThrowPos.position, ThrowPos.rotation);

            StartCoroutine(ThrowCoolDown());
            StartCoroutine(DestroyKnife(tempknife));
        }
    }
    void LastCordinate()
    {
        lookDir = mousePos - ThrowPos.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        ThrowPos.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    private IEnumerator ThrowCoolDown()
    {
        yield return new WaitForSeconds(1f);
        ChargedThrow = true;
    }

    private IEnumerator DestroyKnife(GameObject tempknife)
    {
        yield return new WaitForSeconds(1f);
        Destroy(tempknife);
    }

}