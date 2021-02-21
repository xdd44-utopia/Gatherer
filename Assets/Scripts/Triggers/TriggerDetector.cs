using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public int unitCount;
    private TriggerManager manager;

    private void Start()
    {
        manager = transform.parent.GetComponent<TriggerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Unit")
        {
            manager.totalCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Unit")
        {
            manager.totalCount--;
        }
    }
}
