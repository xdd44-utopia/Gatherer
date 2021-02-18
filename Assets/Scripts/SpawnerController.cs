using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

	public GameObject unitPrefab;
	public int num;

	// Start is called before the first frame update
	void Start() {
		Camera cam = Camera.main;
		float camHeight = cam.orthographicSize;
		float camWidth = camHeight * cam.aspect;
		for (int i=0;i<num;i++) {
			GameObject unit = Instantiate(unitPrefab);
			unit.transform.position = new Vector2(Random.Range(-camWidth, camWidth), Random.Range(-camHeight, camHeight));
		}
	}
}
