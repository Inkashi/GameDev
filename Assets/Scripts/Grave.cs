using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Grave : MonoBehaviour
{
    public GameObject Necr;
    public GameObject Mush;
    private GameObject[] grounds = new GameObject[3];
    int ShieldDestroy, SpawnMush;
    bool aa, ab = false;
    // Start is called before the first frame update
    void Start()
    {
        ShieldDestroy = Random.Range(0, 2);
        SpawnMush = Random.Range(0, 2);
        while (SpawnMush == ShieldDestroy)
        {
            SpawnMush = Random.Range(0, 2);
        }
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            grounds[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!grounds[ShieldDestroy].activeSelf && !aa)
        {
            Destroy(Necr.transform.GetChild(Necr.transform.childCount - 1).gameObject);
            Necr.GetComponent<Enemy>().damageActive = true;
            aa = true;
        }
        if (!grounds[SpawnMush].activeSelf && !ab)
        {
            Instantiate(Mush, Necr.transform.position, Quaternion.identity);
            ab = true;
        }
        if (Necr == null)
        {
            Destroy(gameObject);
        }
    }
}
