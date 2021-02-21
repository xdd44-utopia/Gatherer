using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    [Tooltip("机关需要打开的门")]
    public GameObject[] doors;
    [Tooltip("需要打开门所需球数量")]
    public int requiredNumber = 6;
    public Transform[] allTriggers;
    public int totalCount;
    public bool spawnUnit = false;

    public event Action DoorOpenedHandler;

    private void Start()
    {
        if (spawnUnit)
            allTriggers = new Transform[transform.childCount - 1];
        else
            allTriggers = new Transform[transform.childCount];
        for (int i = 0; i < allTriggers.Length; i++)
        {
            allTriggers[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        if(allTriggers.Length == 1)
        {
            if (totalCount >= requiredNumber)
                DoorOpened();
        }
        else
        {
            bool allSatisfied = true;
            foreach (var item in allTriggers)
            {
                //print("unitcount: " + item.GetComponent<TriggerDetector>().unitCount + " compare: " + requiredNumber / allTriggers.Length);
                //print(item.GetComponent<TriggerDetector>().unitCount >= (requiredNumber / allTriggers.Length));
                allSatisfied &= (item.GetComponent<TriggerDetector>().unitCount >= (requiredNumber / allTriggers.Length));
            }
            if (allSatisfied)
                DoorOpened();
        }
    }
       

    private void DoorOpened()
    {
        DoorOpenedHandler?.Invoke();
        foreach (var item in doors)
        {
            item.GetComponent<DoorOpen>().Open();
        }
        
    }



    private int CountAllUnitNumber()
    {
        int count = 0;
        for (int i = 0; i < allTriggers.Length - 1; i++)
        {
            count += allTriggers[i].GetComponent<TriggerDetector>().unitCount;

        }

        totalCount = count;
        return count;
    }
}
