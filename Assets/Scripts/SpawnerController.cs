using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

	public GameObject[] prefabs;
	public int[] nums;

	// Start is called before the first frame update
	void Start() {
		Camera cam = Camera.main;
		float camHeight = cam.orthographicSize;
		float camWidth = camHeight * cam.aspect;
		for (int i=0;i<prefabs.Length;i++) {
			for (int j=0;j<nums[i];j++) {
				GameObject unit = Instantiate(prefabs[i]);
				unit.transform.position = new Vector2(Random.Range(-camWidth, camWidth), Random.Range(-camHeight, camHeight));
			}
		}
	}
}
