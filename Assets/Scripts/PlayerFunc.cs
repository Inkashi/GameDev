using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunc : MonoBehaviour
{
    public int health = 100;
    public GameObject Player;
   public void TakeDamage(int damage)
	{
     
		health -= damage;
        Debug.Log(health);

		if (health <= 0)
		{
			Die();
		}
	}
    void Die() {
        Destroy(Player);
    }
}
