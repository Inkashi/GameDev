using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLaserControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] lasers;
    public float RotateSpeed; 

    public GameObject lightLaser;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,RotateSpeed*Time.deltaTime);
    }
    
    public void attck() 
    {
        for(int i = 0;i < lasers.Length ; i++) {
            Instantiate(lightLaser, transform.position, lasers[i].rotation);
        }
        
    }
}
