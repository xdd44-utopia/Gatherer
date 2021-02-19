using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Status : MonoBehaviour
{
    public float moveSpeed;
    public float dragSpeed;
    public float followSpeed;
    public float gatherTime;
    public float angleRange;
    public float maxGatherDist;
    public float cooldownTime;
    private const float moveSpeed_def = 0.01f;
    private const float dragSpeed_def = 10f;
    private const float followSpeed_def = 1f;
    private const float gatherTime_def = 0.25f;
    private const float angleRange_def = 0.1f;
    private const float maxGatherDist_def = 2f;
    private const float cooldownTime_def = 2f;
    public float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        setDefalut();
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
            setDefalut();
        }
    }
    void setDefalut()
    {
        moveSpeed = moveSpeed_def;
        dragSpeed=dragSpeed_def;
        followSpeed=followSpeed_def;
        gatherTime=gatherTime_def;
        angleRange=angleRange_def;
        maxGatherDist=maxGatherDist_def;
        cooldownTime=cooldownTime_def;
    }
    public void execute_prop_attract(float remain_time)
    {
        followSpeed *= 1.5f;
        maxGatherDist *= 1.5f;
        timer = remain_time;
        return;
    }
}
