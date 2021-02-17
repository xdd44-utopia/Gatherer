using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject unitPrefab;
	public int num;
	public float Create_Time;
	public bool infinite;

	private float count_Time;
	private float camHeight;
	private float camWidth;
	void Start()
	{
		Camera cam = Camera.main;
		camHeight = cam.orthographicSize;
		camWidth = camHeight * cam.aspect;
		count_Time = Create_Time;
		
		spawn();
	}

	// Update is called once per frame
	void Update()
	{
		count_Time -= Time.deltaTime;
		if (count_Time <= 0 && infinite) {
			spawn();
		}
	}

	void spawn() {
		for (int i = 0; i < num; i++)
		{
			GameObject unit = Instantiate(unitPrefab);
			float Random_x;
			float Random_y;
			do
			{
				Random_x = Random.Range(-2*camWidth, 2*camWidth);
				Random_y = Random.Range(-2*camHeight, 2*camHeight);
			} while (Random_x < camWidth && Random_x > -camWidth && Random_y<camHeight && Random_y>-camHeight);
			unit.transform.position = new Vector2(Random_x, Random_y);
		}
		count_Time = Create_Time;
	}
}
