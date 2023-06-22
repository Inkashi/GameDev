using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpEnemy : MonoBehaviour
{
    public SpriteRenderer spr;
    public Sprite[] sprites;
    private int hpenemy;
    void Start()
    {
        if (gameObject.tag == "Eye")
        {
            hpenemy = gameObject.GetComponent<EyeControll>().health;
            Debug.Log("I have" + hpenemy);
        }
        else
        {
            hpenemy = gameObject.GetComponent<Enemy>().health;
        }
        spr.sprite = sprites[10];
    }

    public void UpdateHealEnemy()
    {
        int heal;
        if (gameObject.tag == "Eye")
        {
            heal = gameObject.GetComponent<EyeControll>().health;
        }
        else
        {
            heal = gameObject.GetComponent<Enemy>().health;
        }
        if (heal < 0)
        {
            spr.sprite = sprites[0];
        }
        else
        {
            int procenthp = (heal * 100) / hpenemy;
            spr.sprite = sprites[procenthp / 10];
        }
    }
}
