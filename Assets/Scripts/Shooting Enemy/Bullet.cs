using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;


///<summary>
///子弹
///</summary>

public class Bullet : MonoBehaviour
{
    public float atk;

    public float moveSpeed;
    [Tooltip("移动速度")]
    private Vector3 destination;
    [Tooltip("攻击距离")]
    public float attackDistance = 2.5f;
    protected RaycastHit2D hit;
    [Tooltip("射线检测层")]
    public LayerMask layer;
    public Vector3 targetPos;

    public event EventHandler<BulletArrivedEventArgs> ArriveTargetPointHandler;

    private void Awake()
    {
    }

    private void Start()
    {
        atk = FindObjectOfType<Gun>().atk;
        CalculateTargetPoint();
    }

    //public void OnReset()
    //{
    //    CalculateTargetPoint();
    //}

    private void Update()
    {
        MoveToTarget(destination);
        if (Vector3.Distance(transform.position, destination) < 0.04f)
            ArriveAtTargetPoint();
    }

    //父有一个方法 父知道调用时机 通过virtual让子调这个方法 如果是两个毫无关系的类可以通过事件调用 
    protected virtual void ArriveAtTargetPoint()
    {
        //if (ArriveTargetPointHandler != null)
        //{
        //    //构建事件参数类
        //    BulletArrivedEventArgs arrivedEventArgs = new BulletArrivedEventArgs()
        //    {
        //        Hit = hit
        //    };
        //    //引发事件
        //    ArriveTargetPointHandler(this, arrivedEventArgs);
        //}
        //回收子弹
        //GameObjectPool.Instance.CollectObject(gameObject);
        Destroy(gameObject);
    }

    //计算目标点
    private void CalculateTargetPoint()
    {
        //print(transform.position);
        //print(targetPos);
        hit = Physics2D.Raycast(transform.position, targetPos - transform.position, attackDistance, layer);
        if (hit.collider != null)
            destination = hit.point;
        else
            destination = transform.position + (targetPos - transform.position).normalized * attackDistance;

    }

    //移动到目标点
    private void MoveToTarget(Vector3 destination)
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        //Debug.DrawLine(transform.position, destination);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Unit")
        {
            collision.GetComponentInChildren<HealthbarController>().getDamaged(atk);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, destination);
        //Gizmos.DrawLine(transform.position, (targetPos - transform.position).normalized * attackDistance);
    }

}

