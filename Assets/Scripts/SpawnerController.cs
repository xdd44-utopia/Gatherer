using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

	public GameObject[] prefabs;
	public int[] nums;
	private GameObject[] pos;

	// Start is called before the first frame update
	void Start() {
		Camera cam = Camera.main;
		float camHeight = cam.orthographicSize;
		float camWidth = camHeight * cam.aspect;

		pos = GameObject.FindGameObjectsWithTag("UnitSpawnPoint");

		for (int i=0;i<prefabs.Length;i++) {
			for (int j=0;j<nums[i];j++) {
				GameObject unit = Instantiate(prefabs[i]);
				unit.transform.position = pos[(int)Random.Range(0, pos.Length - 0.0000001f)].transform.position;
			}
		}
	}
}
