using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    [Tooltip("机关需要打开的门")]
    public GameObject door;
    [Tooltip("需要打开门所需球数量")]
    public int requiredNumber = 6;
    private Transform[] allTriggers;
    public int totalCount;

    private void Start()
    {
        allTriggers = new Transform[transform.childCount - 1];
        for (int i = 0; i < allTriggers.Length; i++)
        {
            allTriggers[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        if (totalCount >= requiredNumber)
            door.GetComponent<DoorOpen>().Open();
    }

    //public int CountAllUnitNumber()
    //{
    //    int count = 0;
    //    for (int i = 0; i < allTriggers.Length - 1; i++)
    //    {
    //        count += allTriggers[i].GetComponent<TriggerDetector>().unitCount;

    //    }
       
    //    totalCount = count;
    //    return count;
    //}
}
