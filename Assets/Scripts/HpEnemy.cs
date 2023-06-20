using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpEnemy : MonoBehaviour
{
    public SpriteRenderer spr;
    public Sprite[] sprites;
    private int hpenemy;
    // Start is called before the first frame update
    void Start()
    {
        hpenemy = gameObject.GetComponent<Enemy>().health;
        spr.sprite = sprites[10];
    }

    public void UpdateHealEnemy()
    {
        int heal = gameObject.GetComponent<Enemy>().health;
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
