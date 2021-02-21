using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTeller : MonoBehaviour
{
	public GameObject cam;
	public GameObject mouse;
	private float timer = 0;
	private int phase = 0;
	//0: start, wait for seconds, then release the second and the third friend units
	//1: wait for three units to gather to lock camera
	//2: wait for player to kill the trial enemy
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		switch (phase) {
			case 0:
				m0();
				break;
			case 1:
				m1();
				break;
			case 2:
				m2();
				break;
		}
	}

	public GameObject unitSpawner1;
	private float time1 = 3f;

	void m0() {
		timer += Time.deltaTime;
		if (timer > time1) {
			timer = 0;
			unitSpawner1.GetComponent<SpawnerController>().activate();
			phase += 1;
			FindObjectOfType<AudioManager>().Play("Theme", 1);
		}
	}

	public GameObject camPosition1;
	void m1() {
		if (mouse.GetComponent<MouseController>().getGatherableCount() == 3) {
			cam.GetComponent<CameraController>().lockCam(camPosition1.transform.position);
			phase += 1;
		}
	}

	public GameObject enemy1;
	void m2() {
		if (enemy1 == null) {
			Debug.Log("killed");
			cam.GetComponent<CameraController>().releaseCam();
			phase += 1;
		}
	}
}
