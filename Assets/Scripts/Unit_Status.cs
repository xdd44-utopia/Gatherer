using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Status : MonoBehaviour
{
    [HideInInspector]
    public float moveSpeed;
    [HideInInspector]
    public float dragSpeed;
    [HideInInspector]
    public float followSpeed;
    [HideInInspector]
    public float gatherTime;
    [HideInInspector]
    public float angleRange;
    [HideInInspector]
    public float maxGatherDist;
    [HideInInspector]
    public float cooldownTime;
    [HideInInspector]
    public float moveSpeed_def = 0.05f;
    public float dragSpeed_def = 20f;
    public float followSpeed_def = 3f;
    public float gatherTime_def = 0.25f;
    public float angleRange_def = 0.1f;
    public float maxGatherDist_def = 10f;
    public float cooldownTime_def = 2f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        setDefault();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            setDefault();
        }
    }
    void setDefault()
    {
        moveSpeed = moveSpeed_def;
        dragSpeed = dragSpeed_def;
        followSpeed = followSpeed_def;
        gatherTime = gatherTime_def;
        angleRange = angleRange_def;
        maxGatherDist = maxGatherDist_def;
        cooldownTime = cooldownTime_def;
    }
    public void execute_prop_attract(float remain_time)
    {
        followSpeed *= 2f;
        maxGatherDist *= 2f;
        timer = remain_time;
        return;
    }
}
