using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    [SerializeField] GameObject healthpotion;
    private int health = 100;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            Instantiate(healthpotion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
