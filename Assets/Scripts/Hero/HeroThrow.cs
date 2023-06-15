using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroThrow : MonoBehaviour
{
    Vector3 mousePos;
    private Animator anim;
    private bool ChargedThrow = true;
    public Transform ThrowPos;
    public GameObject knife;
    public Camera cam;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - ThrowPos.position;
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
            anim.SetTrigger("Throw");
            GameObject tempknife = Instantiate(knife, ThrowPos.position, ThrowPos.rotation);
            ChargedThrow = false;
            StartCoroutine(ThrowCoolDown());
            StartCoroutine(DestroyKnife(tempknife));
        }
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
