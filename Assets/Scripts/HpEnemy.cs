using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpEnemy : MonoBehaviour
{
    public SpriteRenderer spr;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        spr.sprite = sprites[9];
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
            spr.sprite = sprites[heal / 10];
        }
    }
}
