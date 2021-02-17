using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePoint : MonoBehaviour
{
    public GameObject unitPrefab;
    public Camera mainCamera;
    //public float setDistance;

    private GameObject cam;
    private float cam_Width;
    private float cam_Height;
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        //float minDist = setDistance;
        float cam_posx = cam.transform.position.x;
        float cam_posy = cam.transform.position.y;
        float cur_posx = gameObject.transform.position.x;
        float cur_posy = gameObject.transform.position.x;
        cam_Height = mainCamera.orthographicSize;
        cam_Width = cam_Height * Screen.width * 1.0f / Screen.height;
        if (cur_posx<cam_posx+cam_Width && cur_posx>cam_posx-cam_Width && cur_posy<cam_posy+cam_Height && cur_posy>cam_posy-cam_Height)
        {
            GameObject enemy = Instantiate(unitPrefab);
            enemy.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            GameObject.DestroyImmediate(gameObject);
            return;
        }
    }
}
