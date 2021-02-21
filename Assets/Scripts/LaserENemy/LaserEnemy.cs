using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    private float startTime;
    public float shootInterval = 2;
    public float shootDelay = 0.5f;
    public GameObject laser;
    public Transform shootPoint;

    void Update()
    {
        if(startTime < Time.time)
        {
            GameObject laserGO = Instantiate(laser, shootPoint.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("Laser", 1);
            Destroy(laserGO, shootDelay);
            startTime = Time.time + shootInterval;
        }
    }
}
