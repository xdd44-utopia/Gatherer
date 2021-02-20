using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLight : MonoBehaviour
{
   
    void Update()
    {
        Vector3 mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousPos.x, mousPos.y, transform.position.z);
    }
}
