using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject LaserBeam;
    void LasAttck() {
        GameObject Lasers = Instantiate(LaserBeam, attackPoint.position, Quaternion.identity);
    }
}
