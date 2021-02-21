using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prop_splitedwall : MonoBehaviour
{
    public int requireUnits;
    public float minDistance;
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
            if (Vector2.Distance(transform.position, unit.transform.position) < radius+minDistance)
            {
                switch(cnt){
                    case 0:
                    break;
                    case 1:
                    FindObjectOfType<AudioManager>().Play("0", 1);
                    break;
                    case 2:
                    FindObjectOfType<AudioManager>().Play("1", 1);
                    break;
                    case 3:
                    FindObjectOfType<AudioManager>().Play("2", 1);
                    break;
                    case 4:
                    FindObjectOfType<AudioManager>().Play("3", 1);
                    break;
                    default:
                    FindObjectOfType<AudioManager>().Play("4", 1);
                    break;
                }
                cnt++;
            }
        }
        if (cnt >= requireUnits)
        {
            //add music code here
            GameObject.DestroyImmediate(gameObject);
            FindObjectOfType<AudioManager>().Play("DestroyWall", 1);
            return;
        }
    }
}
