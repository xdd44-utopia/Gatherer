using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : MonoBehaviour
{
    public float attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Unit")
        {
            print("!!!!!1");
            collision.GetComponentInChildren<HealthbarController>().getDamaged(attack);
        }
    }
}
