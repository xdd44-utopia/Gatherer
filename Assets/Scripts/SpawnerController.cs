using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

	public GameObject[] prefabs;
	public int[] nums;
	private Transform[] spawnPoints;

	// Start is called before the first frame update
	void Start() {
		Camera cam = Camera.main;
		float camHeight = cam.orthographicSize;
		float camWidth = camHeight * cam.aspect;

		spawnPoints = new Transform[transform.childCount];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
			spawnPoints[i] = transform.GetChild(i);
        }

	}

	public void activate() {
		for (int i=0;i<prefabs.Length;i++) {
			for (int j=0;j<nums[i];j++) {
				Instantiate(prefabs[i], spawnPoints[Random.Range(0, spawnPoints.Length)].position,Quaternion.identity);
			}
		}
	}

	
}
