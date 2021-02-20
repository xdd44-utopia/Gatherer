using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prop_heal : MonoBehaviour
{
    public float remain_time;
    public float amount;
    public int requireUnits;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void execute(float radius)
    {
        int cnt = 0;
        GameObject[] units;
        units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unit in units)
        {
            if (Vector2.Distance(transform.position, unit.transform.position) < radius)
            {
                cnt++;
            }
        }
        if (cnt >= requireUnits)
        {
            foreach (GameObject unit in units)
            {
                unit.GetComponent<UnitController>().getHealed(amount, remain_time);
            }
            GameObject.DestroyImmediate(gameObject);
            return;
        }
    }
}
