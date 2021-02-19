﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public int unitCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Unit")
            unitCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Unit")
            unitCount--;
    }
}
