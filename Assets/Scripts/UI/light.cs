using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    public GameObject ray;
    public GameObject healEffect;
    GameObject hero;
    bool active = false;

    private void Start()
    {
        hero = GameObject.Find("MainHero");
    }
    public void useflower()
    {
        if (!active)
        {
            if (hero.GetComponent<HeroMovement>().health < 100)
            {
                ray.SetActive(true);
                StartCoroutine(HealCD());
                active = true;
            }
        }
    }

    private IEnumerator HealCD()
    {
        for (int i = 0; i < 4; i++)
        {
            Destroy(Instantiate(healEffect, hero.transform.position, Quaternion.identity), 2f);
            if (hero.GetComponent<HeroMovement>().health >= 90)
                hero.GetComponent<HeroMovement>().health = 100;
            else
                hero.GetComponent<HeroMovement>().health += 10;
            yield return new WaitForSeconds(0.8f);
        }
    }
}
