using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float RotateSpeed; 
    private bool Reverse = false;
    private float timer = 0;

    private float reflect = 1f;
    
    void Start() 
    {
       // Reverse = GetComponentInParent<Boss>().reverse;
        Reverse = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().reverse;
        if (Reverse == true) {
            reflect *=-1;
        }
        
    }
    void Update()
    {
        timer+= Time.deltaTime; 
        transform.Rotate(0,0,RotateSpeed*reflect*Time.deltaTime);
        if(timer >=4) {
            Destroy(this.gameObject);
        }
    }
}
