using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

///<summary>
///枪的控制
///</summary>

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    [HideInInspector]
    public Transform firePoint;
    public string firePointName = "FirePoint";
    //private AudioSource audioSource;
    [Tooltip("攻击力")]
    public float atk = 0.4f;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        firePoint = transform.FindChildByName(firePointName);
    }

    public void Fire(Vector3 direction)
    {
        //Quaternion.LookRotation(方向) => 返回z轴指向该方向的旋转
        GameObject bulletGO = Instantiate(bullet, firePoint.position, Quaternion.identity);
        //GameObject bulletGO = GameObjectPool.Instance.CreateObject("bullet", bullet, firePoint.position, Quaternion.LookRotation(direction));
        //给子弹传递攻击力
        bulletGO.GetComponent<Bullet>().targetPos = direction;
        bulletGO.GetComponent<Bullet>().atk = atk;
        //audioSource.Play();
    }

}
