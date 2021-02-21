using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prop_splitedwall : MonoBehaviour
{
    public int requireUnits;
    public float minDistance;

    public event Action WallDestroyedHandler;
    
    public void execute(float radius)
    {
        int cnt = 0;
        GameObject[] units;
        units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unit in units)
        {
            if (Vector2.Distance(transform.position, unit.transform.position) < radius+minDistance)
            {
                cnt++;
            }
        }
        if (cnt >= requireUnits)
        {
            //add music code here
            WallDestroyedHandler?.Invoke();
            GameObject.DestroyImmediate(gameObject);
            return;
        }
    }
}
