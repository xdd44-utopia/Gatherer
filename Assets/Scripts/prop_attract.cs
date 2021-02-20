using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prop_attract : MonoBehaviour
{
    public float remain_time;
    //public float minDistance;
    public int requireUnits;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*GameObject cursor=GameObject.Find("Cursor");
        if (Vector2.Distance(transform.position, cursor.transform.position)<minDistance)
        {
            GameObject[] units;
            units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject unit in units)
            {
                unit.GetComponent<Unit_Status>().execute_prop_attract(remain_time);
            }
            GameObject.DestroyImmediate(gameObject);
            return;
        }*/
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
                unit.GetComponent<Unit_Status>().execute_prop_attract(remain_time);
            }
            GameObject.DestroyImmediate(gameObject);
            return;
        }
    }
}
