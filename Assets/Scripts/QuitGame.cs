using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
