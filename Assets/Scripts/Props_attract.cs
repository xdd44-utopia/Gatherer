using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Props_attract : MonoBehaviour
{
    public float remain_time;
    public float minDistance;

    void Start()
    {
        
    }

    
    void Update()
    {
        GameObject cursor = GameObject.Find("Cursor");
        if(Vector2.Distance(transform.position, cursor.transform.position) < minDistance)
        {
            //gameObject.SetActive(false);

            GameObject[] units;
            units = GameObject.FindGameObjectsWithTag("Unit");
            foreach (GameObject unit in units)
            {
                unit.SendMessage("execute_props_attract", remain_time);
            }
            GameObject.DestroyImmediate(gameObject);
            return;
        }
    }
}
