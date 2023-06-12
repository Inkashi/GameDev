using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroThrow : MonoBehaviour
{
    Vector3 mousePos;
    public Animator anim;   
     public Transform ThrowPos;
     public GameObject knife;
    public Camera cam;
    
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - ThrowPos.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        ThrowPos.rotation = Quaternion.Euler(0f,0f,angle-90f);
        
         if (Input.GetMouseButtonDown(1)) {
            anim.SetTrigger("Throw");
           // Throw();
         }

            
    }
    public void Throw() {
        Instantiate(knife, ThrowPos.position, ThrowPos.rotation);
    }
   
}
